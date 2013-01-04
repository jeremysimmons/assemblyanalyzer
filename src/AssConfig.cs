using System;
using System.Xml;
using System.Collections;

class AssConfig {

	/*static void Main() {
		AssConfig a = new AssConfig();
		IEnumerator i = a.loadBundles().GetEnumerator();
		while (i.MoveNext()) {
			String s = (String)i.Current;
			Console.WriteLine(s);
		}
	}*/
	
	public void loadBundles(IList assemblyList, IList outputTypes) {

		XmlTextReader reader = new XmlTextReader("./assanalyzerconfig.xml");
		//ArrayList list = new ArrayList();
		bool loadingAssemblies = false;
		bool loadingOutput = false;
		bool loadingType = false;
		bool loadingFile = false;
		OutputType outputType;
		String type = "";
		String file = "";
		while (reader.Read()) {
			//Console.WriteLine(reader.Name);
			switch (reader.NodeType) {
				case XmlNodeType.Element: 
					if (reader.Name == "bundles") {
						loadingAssemblies = true;
					}
					if (reader.Name == "output") {
						loadingOutput = true;
					}
					if (reader.Name == "type") {
						loadingType = true;
					}
					if (reader.Name == "file") {
						loadingFile = true;
					}
					//Console.WriteLine(reader.Name);
					break;
				case XmlNodeType.Text:
					//Console.WriteLine(reader.Value);
					if (loadingAssemblies) {
						assemblyList.Add(reader.Value);
					}
					/*if (loadingOutput) {
						outputTypes.Add(reader.Value);
					}*/
					if (loadingType) {
						type = reader.Value;
					}
					if (loadingFile) {
						file = reader.Value;
					}
					break;
				case XmlNodeType.EndElement: 
					if (reader.Name == "bundles") {
						loadingAssemblies = false;
					}
					if (reader.Name == "output") {
						outputTypes.Add(new OutputType(file, type));
						loadingOutput = false;
					}
					if (reader.Name == "type") {
						loadingType = false;
					}
					if (reader.Name == "file") {
						loadingFile = false;
					}
					//Console.WriteLine(reader.Name);
					break;
			}
		}
		//return list;
		
	}


}