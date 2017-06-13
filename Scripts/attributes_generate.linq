<Query Kind="Program">
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
	var attributes = new Dictionary<string, string[]> {
		{ "Attributes", new string[] {
			"Strength",
			"Speed",
			"Fortitude",
			"Resolve",
			"Faith",
			"Intelligence"
		}},
		{ "Combat Skills", new string[] {
			"Archery",
			"Brawl",
			"Dodge",
			"Firearms",
			"Heavy Weapons",
			"Light Weapons",
			"Medium Weapons",
			"Parry",
			"Shields",
			"Steady"
		}},
		{ "Physical Skills", new string[] {
			"Finesse",
			"Grit",
			"Stealth",
			"Mobility"
		}},
		{ "Professional Skills", new string[] {
			"Mercantile",
			"Farming",
			"Forestry",
			"Herbalism",
			"Mining",
			"Trapping",
			"Leatherworking",
			"Woodworking",
			"Metalworking",
			"Tailoring",
			"Mechanics",
			"Apothecary"
		}},
		{ "Life Skills", new string[] {
			"Sincerity",
			"Intimidate",
			"Seduce",
			"Discipline",
			"Persuade",
			"Performance",
			"Streetwise",
			"Survival",
			"Etiquette",
			"Perception",
			"Academics",
			"Leadership",
			"Liturgy"			
		}},
		{ "Perks", new string[] {
			"Alacrity",
			"Ally",
			"Backing",
			"Boon",
			"Fame",
			"Impeccable Memory",
			"Journeyman",
			"Magnetism",
			"Nobody's Fool",
			"Quick Healer",
			"Quiet",
			"Retainer",
			"Slow Bleeder",
			"Spiritual Prodigy",
			"Wealth",
			"Well-Equipped",
			"Workhorse",
			"The Spark",
			"Respected",
			"Hardy",
			"Light Sleeper",
			"Street Savvy",
			"Entrepreneur",
			"Connections", 
			"Classical Education",
			"Highborn",
			"Ice-Hardened",
			"Branded",
			"Tough as Nails", 
			"Collected",
			"Veteran",
			"Loyalty",
			"Humorless",
			"Gnosis",
			"Pious",
			"Natural Linguist",
			"Caravanserai",
			"Magical Wonder",
			"Hard Drinker",
			"Oral Tradition",
			"Ancestral Moorsword",
			"Trade Guild Amici",
			"In Flagrante",
			"Daredevil",
			"Silver Tongue",
			"Elitist",
			"Pistolier"
		}},
		{ "Flaws", new string[] {
			"Beholden",
			"Corpse in the Closet",
			"Dirt Poor",
			"Duty",
			"Enemy",
			"Craven",
			"Cursed",
			"Hedonist",
			"Honor Code",
			"Infamy",
			"Memorable",
			"Naive",
			"Old Wounds",
			"One Eyed Jack",
			"One Foot in the Grave",
			"Pure of Heart",
			"Sick in the Head",
			"Vainglorious",
			"Ward",
			"Wicked",
			"Dainty",
			"Hayseed",
			"Odious",
			"Debt",
			"Entitlement",
			"Thrall of the Old Gods",
			"Pig-Headed",
			"Bigoted",
			"Outspoken Heathen",
			"Harsh Temper",
			"Vendetta",
			"Fop"
		}},
		{ "Traits", new string[] {
			"Abhorrent",
			"Alcoholic",
			"Bravado",
			"Foolish Heart",
			"Death Wish",
			"Finesse Fighter",
			"Heavy Handed", 
			"Renegade"
		}},
		{ "Cultures", new string[] {
			"Capacionne",
			"Dunnick",
			"Gothic",
			"Hestrali",
			"Njordic",
			"Rogalian",
			"Shariqyn"
		}},
		{ "Social Classes", new string[] {
			"Scum",
			"Peasant",
			"Merchant",
			"Gentry"
		}}
	};
	
	var socialClassSkills = new Dictionary<string, string[]> {
		{ "Scum", new string[] {
			"Streetwise",
			"Sincerity",
			"Intimidate",
			"Finesse",
			"Stealth"
		}},
		{ "Peasant", new string[] {
			"Perception",
			"Liturgy",
			"Grit",
			"Survival",
			"Farming",
			"Forestry",
			"Herbalism",
			"Trapping",
			"Mining"
		}},
		{ "Merchant", new string[] {
			"Mercantile",
			"Perception",
			"Survival",
			"Sincerity",
			"Leatherworking",
			"Woodworking",
			"Metalworking",
			"Tailoring",
			"Mechanics"
		}},
		{ "Gentry", new string[] {
			"Medium Weapons",
			"Etiquette",
			"Academics",
			"Intimidate",
			"Leadership"
		}}
	};
	
	var cultureSkills = new Dictionary<string, string[]> {
		{ "Capacionne", new string[] {
			"Firearms",
			"Trapping",
			"Mechanics",
			"Seduce",
			"Persuade"
		}},
		{ "Dunnick", new string[] {
			"Brawl",
			"Grit",
			"Herbalism",
			"Mining",
			"Apothecary"
		}},
		{ "Gothic", new string[] {
			"Discipline",
			"Farming",
			"Medium Weapons",
			"Leadership",
			"Liturgy"
		}},
		{ "Hestrali", new string[] {
			"Mercantile",
			"Mobility",
			"Seduce",
			"Finesse",
			"Performance"
		}},
		{ "Njordic", new string[] {
			"Survival",
			"Grit",
			"Intimidate",
			"Performance",
			"Forestry"
		}},
		{ "Rogalian", new string[] {
			"Archery",
			"Etiquette",
			"Metalworking",
			"Discipline",
			"Perception"
		}},
		{ "Shariqyn", new string[] {
			"Academics",
			"Mercantile",
			"Survival",
			"Persuade",
			"Liturgy"
		}}
	};
	var exoterics = new string[] {
		"Architecture",
		"Astrology",
		"Anatomy",
		"Sociology",
		"Zoology",
		"Physics",
		"Geology",
		"Philosophy",
		"Mathematics",
		"Logic",
		"Rhetoric",
		"Physiology",
		"Ecology",
		"Theology",
		"Hydraulics",
		"Meteorology",
		"Horology",
		"Economics",
		"Botany",
		"Archaelogy",
		"Geography",
		"Linguistics",
		"Civics",
		"History",
		"Psychology",
		"Metallurgy",
		"Library Science",
		"Law",
		"Pneumatics",
		"Logistics",
		"Strategy"
	};
	
	var esoterics = new Dictionary<string, string[]> {
		{ "Circumlocution", new string[] {
			"Rhetoric",
			"Linguistics"
		}},
		{ "Ergodocity", new string[] {
			"Mathematics", "Philosophy"
		}},
		{ "Celestial Geometry", new string[] {
			"Mathematics", "Astrology"
		}},
		{ "Metric Tensors", new string[] {
			"Mathematics", "Physics"
		}},
		{ "Tectonics", new string[] {
			"Geology", "Physics"
		}},
		{ "Thermionics", new string[] {
			"Physics", "Psychology"
		}},
		{ "Solipsism", new string[] {
			"Psychology", "Philosophy"
		}},
		{ "Astromantics", new string[] {
			"Pneumatics", "Psychology"
		}},
		{ "Capacitance", new string[] {
			"Physics", "Hydraulics"
		}},
		{ "Kairos", new string[] {
			"Horology", "Astrology"
		}},
		{ "Scalar Forces", new string[] {
			"Physics", "Architecture"
		}},
		{ "Epitaxy", new string[] {
			"Geology", "Architecture"
		}},
		{ "Syllogistics", new string[] {
			"Logic", "Rhetoric"
		}},
		{ "Homology", new string[] {
			"Logic", "Mathematics"
		}},
		{ "Hermeneutics", new string[] {
			"Philosophy", "Theology", "Linguistics"
		}},
		{ "Metagraphy", new string[] {
			"Linguistics", "Psychology"
		}},
		{ "Thyristors", new string[] {
			"Physics", "Pneumatics"
		}},
		{ "Fetishism", new string[] {
			"Theology", "Psychology"
		}},
		{ "Chirology", new string[] {
			"Physiology", "Anatomy"
		}},
		{ "Angular Frequency", new string[] {
			"Celestial Geometry"
		}},
		{ "Exegesis", new string[] {
			"Hermeneutics"
		}},
		{ "Tautology", new string[] {
			"Homology", "Syllogistics"
		}},
		{ "Teleology", new string[] {
			"Hermeneutics"
		}},
		{ "Pyroclastics", new string[] {
			"Thermionics", "Psychology"
		}},
		{ "Cthonics", new string[] {
			"Tectonics", "Psychology"	
		}},
		{ "Bathylics", new string[] {
			"Solipsism", "Horology"
		}},
		{ "Fulminology", new string[] {
			"Astromantics",
			"Meteorology"
		}},
		{ "Arrondissement", new string[] {
			"Homology", "Tectonics"
		}},
		{ "Resonance", new string[] {
			"Capacitance"
		}},
		{ "Sidereal Time", new string[] {
			"Celestial Geometry",
			"Horology"
		}},
		{ "Epiphenomina", new string[] {
			"Resonance"
		}},
		{ "Energetics", new string[] {
			"Scalar Forces"
		}},
		{ "Irregular Recursions", new string[] {
			"Angular Frequency"
		}},
		{ "Polymathematics", new string[] {
			"Angular Frequency", "Metric Tensors"
		}},
		{ "Eisegesis", new string[] {
			"Exegesis"
		}},
		{ "Metanymics", new string[] {
			"Teleology"
		}},
		{ "Interior Encoding", new string[] {
			"Circumlocution", "Solipsism"
		}},
		{ "Derivative Geometrics", new string[] {
			"Sidereal Time"
		}},
		{ "Quaternion Invocations", new string[] {
			"Sidereal Time", "Angular Frequency"	
		}},
		{ "Memetic Resonance", new string[] {
			"Fetishism", "Resonance"
		}},
		{ "Identity Negotiation", new string[] {
			"Metagraphy", "Exegesis"
		}},
		{ "Photonics", new string[] {
			"Physics"
		}},
		{ "Deimotics", new string[] {
			"Psychology"
		}},
		{ "Radial Reactions", new string[] {
			"Angular Frequency", "Thermionics"
		}},
		{ "Tension", new string[] {
			"Geology"
		}},
		{ "Impetus", new string[] {
			"Meteorology"
		}},
		{ "Seismology", new string[] {
			"Geology"
		}},
		{ "Confabulonics", new string[] {
			"Psychology"
		}},
		{ "Dolor", new string[] {
			"Philosophy"
		}},
		{ "Nihilistics", new string[] {
			"Theology"
		}},
		{ "Pyrolysis", new string[] {
			"Pyroclastics", "Physiology"
		}},
		{ "Tellurics", new string[] {
			"Cthonics", "Architecture"
		}},
		{ "Sempiternity", new string[] {
			"Bathylics", "Kairos"
		}},
		{ "Turbidity", new string[] {
			"Fulminology", "Ergodocity"
		}},
		{ "Eigenvalues", new string[] {
			"Irregular Recursions", "Quaternion Invocations"
		}},
		{ "Eschatology", new string[] {
			"Deimotics", "Sempiternity"
		}},
		{ "Spectrasonics", new string[] {
			"Resonance", "Quaternion Invocations", "Energetics"
		}},
		{ "Umbral Calculus", new string[] {
			"Polymathematics", "Epiphenomina", "Syllogistics"
		}},
		{ "Thanatology", new string[] {
			"Deimotics", "Memetic Resonance"
		}},
		{ "Ontology", new string[] {
			"Metanymics", "Eisegesis", "Identity Negotiation"
		}},
		{ "Semiosis", new string[] {
			"Logic", "Memetic Resonance"
		}},
		{ "Salience", new string[] {
			"Interior Encoding", "Confabulonics", "Identity Negotiation"
		}}
	};
	
	string.Format("INSERT INTO attribute_types(id, name) VALUES {0};", string.Join(", ", attributes.Keys.Select((key, id) => string.Format("({0}, \"{1}\")", id + 1, key)))).Dump();
	string.Format("INSERT INTO attribute_types(name) VALUES('Esoterics'), ('Exoterics');").Dump();
	
	int currentIndex = 1;
	foreach(var key in attributes.Keys) {
		string.Format("INSERT INTO attributes(attribute_name, attribute_type_id) VALUES {0};",
			string.Join(", ", attributes[key].Select(val => string.Format("(\"{0}\", {1})", val, currentIndex.ToString())))).Dump();
		currentIndex++;
	}
	
	Console.Write("Paste what's currently in the console to the DB, press enter when ready to continue...");
	Console.Read();
	"".Dump();
	"INSERT INTO advancement_lists(name,is_chargen_only) VALUES('Social Status', 1), ('Culture', 1), ('Social Status Skills', 1), ('Culture Skills', 1), ('Esoterics', 0), ('Exoterics', 0);".Dump();
	"".Dump();
	
	var exoteric_id = Attribute_types.FirstOrDefault (a => a.Name == "Exoterics");
	string.Format("INSERT INTO attributes(attribute_name, attribute_type_id) VALUES {0};",
		string.Join(", ", exoterics.Select (e => string.Format("(\"{0}\", {1})", e, exoteric_id.Id)))).Dump();
		
	var esoteric_id = Attribute_types.FirstOrDefault(a => a.Name == "Esoterics");
	string.Format("INSERT INTO attributes(attribute_name, attribute_type_id) VALUES {0};",
		string.Join(", ", esoterics.Keys.Select(e => string.Format("(\"{0}\", {1})", e, esoteric_id.Id)))).Dump();
		
	Console.Write("Paste what's currently in the console to the DB, press enter when ready to continue...");
	Console.Read();
	"".Dump();
		
	int last_list_attribute_id = 1;
	foreach(var social in socialClassSkills.Keys) {
		var social_row = Attributes.FirstOrDefault (s => s.Attribute_name == social);
		if(social_row == null) {
			throw new Exception("Unable to find social status: " + social);
		}
		
		string.Format("INSERT INTO advancement_list_attributes (id, advancement_list_id, attribute_id) VALUES({0}, {1}, {2});", last_list_attribute_id,
			1, social_row.Id).Dump();
		last_list_attribute_id++;
	}
	
	foreach(var culture in cultureSkills.Keys) {
		var culture_row = Attributes.FirstOrDefault(a => a.Attribute_name == culture);
		if(culture_row == null) {
			throw new Exception("Unable to find culture: " + culture);
		}
		string.Format("INSERT INTO advancement_list_attributes (id, advancement_list_id, attribute_id) VALUES({0}, {1}, {2});", last_list_attribute_id,
			2, culture_row.Id).Dump();
		last_list_attribute_id++;
	}
	
	foreach(var culture in cultureSkills.Keys) {
		var culture_row = Attributes.FirstOrDefault(a => a.Attribute_name == culture);
		if(culture_row == null) {
			throw new Exception("Unable to find culture: " + culture);
		}
		foreach(var skill in cultureSkills[culture]) {
			var skill_id = Attributes.FirstOrDefault (s => s.Attribute_name == skill);
			if(skill_id == null) {
				throw new Exception("Unable to find skill: " + skill);
			}
			string.Format("INSERT INTO advancement_list_attributes (id, advancement_list_id, attribute_id) VALUES({0}, 4, {1});", last_list_attribute_id, skill_id.Id).Dump();
			string.Format("INSERT INTO advancement_list_requirements(advancement_list_attribute_id, attribute_requirement_id) VALUES({0}, {1});", last_list_attribute_id, culture_row.Id).Dump();
			
			last_list_attribute_id++;			
		}
	}
		
	foreach(var social in socialClassSkills.Keys) {
		var social_row = Attributes.FirstOrDefault (s => s.Attribute_name == social);
		if(social_row == null) {
			throw new Exception("Unable to find social status: " + social);
		}
		
		foreach(var skill in socialClassSkills[social]) {
			var skill_id = Attributes.FirstOrDefault (s => s.Attribute_name == skill);
			if(skill_id == null) {
				throw new Exception("Unable to find skill: " + skill);
			}
			
			string.Format("INSERT INTO advancement_list_attributes (id, advancement_list_id, attribute_id) VALUES({0}, 4, {1});", last_list_attribute_id, skill_id.Id).Dump();
			string.Format("INSERT INTO advancement_list_requirements(advancement_list_attribute_id, attribute_requirement_id) VALUES({0}, {1});", last_list_attribute_id, social_row.Id).Dump();
			
			last_list_attribute_id++;
		}
	}
		
	(new string('=', 40) + "Esoterics" + new string('=', 40)).Dump();
	
	foreach(var esoteric in esoterics.Keys) {
		var esoteric_ids = Attributes.FirstOrDefault (e => e.Attribute_name == esoteric);
		if(esoteric_ids == null) {
			throw new Exception("Unable to find esoteric: " + esoteric);
		}
		string.Format("INSERT INTO advancement_list_attributes (id, advancement_list_id, attribute_id) VALUES({0}, 5, {1});", last_list_attribute_id, esoteric_ids.Id).Dump();
		foreach(var req in esoterics[esoteric]) {
			var requirement_id = Attributes.FirstOrDefault(s => s.Attribute_name == req);
			if(requirement_id == null) {
				throw new Exception("Unable to find requirement: " + req);
			}
			string.Format("INSERT INTO advancement_list_requirements(advancement_list_attribute_id, attribute_requirement_id) VALUES({0}, {1});", last_list_attribute_id, requirement_id.Id).Dump();
		}
		
		last_list_attribute_id++;
	}
	
	(new string('=', 40) + "Exoterics" + new string('=', 40)).Dump();
	foreach(var exoteric in exoterics) {
		var exoteric_ids = Attributes.FirstOrDefault (e => e.Attribute_name == exoteric);
		string.Format("INSERT INTO advancement_list_attributes(id, advancement_list_id, attribute_id) VALUES({0}, 6, {1});", last_list_attribute_id, exoteric_ids.Id).Dump();
		last_list_attribute_id++;
	}
	
}

// Define other methods and classes here
