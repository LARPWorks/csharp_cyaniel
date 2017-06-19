using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Factories;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace LARPWorks.Cyaniel.Features.Debug
{
#if DEBUG
    public class DebugModule : CyanielModule
    {
        public DebugModule(IDbFactory dbFactory) : base("Debug")
        {
            Get["/"] = paramaters => Response.AsRedirect("/Debug/Index");
            Get["/Index"] = parameters => View["Index.cshtml", GetViewModel<BaseCyanielViewModel>()];
            Post["/populate_db"] = parameters =>
            {
                var model = this.Bind<BaseCyanielViewModel>();

                // Load the JSON facts..
                var assembly = Assembly.GetExecutingAssembly();
                DebugFactsData data;
                using (var reader = new StreamReader(assembly.GetManifestResourceStream("LARPWorks.Cyaniel.Features.Debug.FactsData.json")))
                {
                    string json = reader.ReadToEnd();
                    data = JsonConvert.DeserializeObject<DebugFactsData>(json);
                }
                try
                {
                    using (var db = dbFactory.Create())
                    {
                        // This is just an internal cache of how the inserts go
                        // for all facts. It speeds up the rest of the creation process.
                        Dictionary<string,int> factMappings = new Dictionary<string, int>();

                        // Insert fact types and facts...
                        foreach (var factType in data.Facts.Keys)
                        {
                            ulong primaryKey = (ulong)db.Insert(new FactType {Name = factType});
                            foreach (var fact in data.Facts[factType])
                            {
                                var factKey = (ulong) db.Insert(new Fact {FactTypeId = (int) primaryKey, Name = fact});
                                factMappings.Add(fact, (int)factKey);
                            }
                        }

                        // Insert all the advancement lists...
                        foreach (var advancementList in data.AdvancementLists.Keys)
                        {
                            ulong primaryKey = (ulong) db.Insert(new AdvancementList {Name = advancementList});
                            foreach (var fact in data.AdvancementLists[advancementList])
                            {
                                if (!factMappings.ContainsKey(fact))
                                {
                                    throw new Exception(string.Format("Unable to find key {0} in fact dictionary", fact));
                                }

                                var factKey = factMappings[fact];
                                ulong advancementFactId = (ulong)db.Insert(new AdvancementListFact
                                {
                                    AdvancementListId = (int) primaryKey,
                                    FactId = factKey,
                                    IsStaffOnly = false
                                });
                            }
                        }

                        // Working on adding all the 'modifiers' in the advancement lists
                        // for all social statuses.
                        foreach (var socialStatus in data.SocialStatusSkills.Keys)
                        {
                            var factKey = factMappings[socialStatus];
                            foreach (var skill in data.SocialStatusSkills[socialStatus])
                            {
                                var skillKey = factMappings[skill];
                                var advancementListFactKey =
                                    db.Single<AdvancementListFact>(
                                        "SELECT * FROM AdvancementListFacts WHERE FactId=@0 AND AdvancementListId=@1", skillKey,
                                        (int)AdvancementListEnum.SocialStatusSkills).Id;

                                db.Insert(new AdvancementListFactModifier
                                {
                                    AdvancementListFactId = advancementListFactKey,
                                    FactRequirementId = factKey
                                });
                            }
                        }

                        // Working on adding all the 'modifiers' in the advancement lists
                        // for all cultures
                        foreach (var culture in data.CultureSkills.Keys)
                        {
                            var factKey = factMappings[culture];
                            foreach (var skill in data.CultureSkills[culture])
                            {
                                var skillKey = factMappings[skill];
                                var advancementListFactKey =
                                    db.Single<AdvancementListFact>(
                                        "SELECT * FROM AdvancementListFacts WHERE FactId=@0 AND AdvancementListId=@1", skillKey,
                                        (int)AdvancementListEnum.CultureSkills).Id;

                                db.Insert(new AdvancementListFactModifier
                                {
                                    AdvancementListFactId = advancementListFactKey,
                                    FactRequirementId = factKey
                                });
                            }
                        }

                        // This code is for adding all the gates.
                        AddGates(db, factMappings, data.EsotericGates, AdvancementListEnum.Esoterics);
                        AddGates(db, factMappings, data.PerkGates, AdvancementListEnum.Perks);
                        AddGates(db, factMappings, data.FlawGates, AdvancementListEnum.Flaws);
                        AddGates(db, factMappings, data.SkillGates, AdvancementListEnum.Skills);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.ValidationError = e.Message
                        + Environment.NewLine
                        + e.StackTrace;
                    return View["Index.cshtml", model];
                }

                ViewBag.ValidationMessage = "Database repopulated successfully!";
                return View["Index.cshtml", model];
            };
            Post["/clear_db"] = parameters =>
            {
                var model = this.Bind<BaseCyanielViewModel>();

                try
                {
                    using (var db = dbFactory.Create())
                    {
                        var tables = db.Fetch<string>("SELECT table_name FROM information_schema.tables " +
                                                      "WHERE table_schema = 'larpworks';");
                        foreach (var table in tables)
                        {
                            db.Execute("SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE " + table + "; SET FOREIGN_KEY_CHECKS = 1;");
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.ValidationError = e.Message;
                    return View["Index.cshtml", model];
                }

                ViewBag.ValidationMessage = "Database cleared!";
                return View["Index.cshtml", model];
            };
            Post["/random_character"] = parameters =>
            {
                var model = this.Bind<BaseCyanielViewModel>();

                ViewBag.ValidationMessage = "New character generated with ID: " + 0;
                return View["Index.cshtml", model];
            };
        }

        public void AddGates(Database db, Dictionary<string, int> factMappings, 
            Dictionary<string, string[]> gates, AdvancementListEnum advancementList)
        {
            foreach (var fact in gates.Keys)
            {
                var factKey = factMappings[fact];
                var advancementFactKey =
                    db.Single<AdvancementListFact>(
                        "SELECT * FROM AdvancementListFacts WHERE FactId=@0 AND AdvancementListId=@1",
                        factKey, (int)advancementList).Id;
                foreach (var requirement in gates[fact])
                {
                    if (!factMappings.ContainsKey(requirement))
                    {
                        throw new Exception("Unable to find fact mapping for " + requirement);
                    }

                    var requirementKey = factMappings[requirement];
                    db.Insert(new AdvancementListFactGate
                    {
                        AdvancementListFactId = advancementFactKey,
                        RequiredFactId = requirementKey
                    });
                }
            }
        }
    }
#endif
}
