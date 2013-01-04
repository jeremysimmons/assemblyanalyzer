using System;
using System.Collections;
using System.Text;
using System.IO;

    class Analyzer
    {
        static void Main(string[] args) {
			AssConfig config = new AssConfig();

			IList assemblyList = new ArrayList();
			IList outputList = new ArrayList();
			config.loadBundles(assemblyList, outputList);
			AssemblyBundle assemblyBundle = new AssemblyBundle(assemblyList);

			IEnumerator outputEnum = outputList.GetEnumerator();
			while (outputEnum.MoveNext()) {
				//String outputType = (String)outputEnum.Current;
				//Console.WriteLine(outputEnum.Current.GetType());
				OutputType outputType = (OutputType)outputEnum.Current;
				if (outputType.getOutputType() == "All") {
					showAllAssemblyDependencies(assemblyBundle, outputType.getFile());
				} else if (outputType.getOutputType() == "Analyzed") {
					showAllAnalyzedAssemblyDependencies(assemblyBundle, outputType.getFile());
				} else if (outputType.getOutputType() == "xml") {
					outputXml(assemblyBundle, outputType.getFile());
				}
			}
        }

        public static void showAllAssemblyDependencies(AssemblyBundle assemblyBundle, String file)
        {
			//Console.Write("Enter grph output file (All): ");
			//using (StreamWriter sw = new StreamWriter(Console.ReadLine())) {
			using (StreamWriter sw = new StreamWriter(file)) {
				sw.WriteLine("digraph G {");
				IEnumerator a = assemblyBundle.getEnum();
				while (a.MoveNext()) {
					IEnumerator assemblies = ((IAssembly)a.Current).getOutgoingDependencies().GetEnumerator();
					while (assemblies.MoveNext()) {
						sw.WriteLine( ((IAssembly)a.Current).getAssemblyName() + " -> " + assemblies.Current);
					}
				}
				sw.WriteLine("}");
			}
        }

		public static void showAllAnalyzedAssemblyDependencies(AssemblyBundle assemblyBundle, String file) {
			//Console.Write("Enter grph output file (Analyzed): ");
			//using (StreamWriter sw = new StreamWriter(Console.ReadLine())) {
			using (StreamWriter sw = new StreamWriter(file)) {
				sw.WriteLine("digraph G {");
				IEnumerator a = assemblyBundle.getEnum();
				while (a.MoveNext()) {
					IEnumerator assemblies = ((IAssembly)a.Current).getAnalyzedOutgoingDependencies().GetEnumerator();
					while (assemblies.MoveNext()) {
						sw.WriteLine( ((IAssembly)a.Current).getAssemblyName() + " -> " + ((IAssembly)assemblies.Current).getAssemblyName());
					}
				}
				sw.WriteLine("}");
			}
		
		
			/*IEnumerator assemblies = assembly.getAnalyzedOutgoingDependencies().GetEnumerator();
			while (assemblies.MoveNext()) {
				sw.WriteLine(assembly.getAssemblyName() + " -> " + ((IAssembly)assemblies.Current).getAssemblyName());
			}*/
			/*Console.WriteLine(assembly.getAssemblyName() + " - Class Count: " + assembly.getClassCount());
			Console.WriteLine(assembly.getAssemblyName() + " - Abstract Class Count: " + assembly.getAbstractClassCount());
			Console.WriteLine(assembly.getAssemblyName() + " - Abstractness: " + assembly.calculateMetrics().calculateAbstractness());
			Console.WriteLine(assembly.getAssemblyName() + " - Instability: " + assembly.calculateMetrics().calculateInstability());
			Console.WriteLine(assembly.getAssemblyName() + " - Afferent Coupling: " + assembly.calculateMetrics().calculateAfferentCoupling());
			Console.WriteLine(assembly.getAssemblyName() + " - Efferent Coupling: " + assembly.calculateMetrics().calculateEfferentCoupling());
			Console.WriteLine(assembly.getAssemblyName() + " - Distance: " + assembly.calculateMetrics().calculateInstability());*/
		}

		public static void outputXml(AssemblyBundle bundle, String file) {
			//Console.Write("Enter xml output file: ");
			//using (StreamWriter sw = new StreamWriter(Console.ReadLine())) {
			using (StreamWriter sw = new StreamWriter(file)) {
				sw.WriteLine("<?xml version=\"1.0\"?>");
				sw.WriteLine("<assanalyzer>");
				sw.WriteLine("<Assemblies>");
				IEnumerator assemblies = bundle.getEnum();
				while (assemblies.MoveNext()) {
					IAssembly assembly = (IAssembly)assemblies.Current;
					Metrics metrics = assembly.calculateMetrics();
					sw.WriteLine(tab(1) + "<Assembly name=\"" + assembly.getAssemblyName() + "\">");
					sw.WriteLine(tab(2) + "<Summary>");
					sw.WriteLine(tab(3) + "<Statistics>");
					sw.WriteLine(tab(4) + "<ClassCount>" + assembly.getClassCount() + "</ClassCount>");
					sw.WriteLine(tab(4) + "<AbstractClassCount>" + assembly.getAbstractClassCount() + "</AbstractClassCount>");
					sw.WriteLine(tab(3) + "</Statistics>");
					
					sw.WriteLine(tab(3) + "<Metrics>");
					sw.WriteLine(tab(4) + "<Abstractness>" + metrics.calculateAbstractness() + "</Abstractness>");
					sw.WriteLine(tab(4) + "<Efferent>" + metrics.calculateEfferentCoupling() + "</Efferent>");
					sw.WriteLine(tab(4) + "<Afferent>" + metrics.calculateAfferentCoupling() + "</Afferent>");
					sw.WriteLine(tab(4) + "<Instability>" + metrics.calculateInstability() + "</Instability>");
					sw.WriteLine(tab(4) + "<Distance>" + metrics.calculateDistance() + "</Distance>");
					sw.WriteLine(tab(3) + "</Metrics>");
					
					sw.WriteLine(tab(3) + "<OutgoingDependencies>");
					IEnumerator outgoingDependencies = assembly.getAnalyzedOutgoingDependencies().GetEnumerator();
					while (outgoingDependencies.MoveNext()) {
						sw.WriteLine(tab(4) + "<Assembly>" + ((IAssembly)outgoingDependencies.Current).getAssemblyName() + "</Assembly>");
					}
					sw.WriteLine(tab(3) + "</OutgoingDependencies>");
					
					sw.WriteLine(tab(3) + "<IncomingDependencies>");
					IEnumerator incomingDependencies = assembly.getAnalyzedIncomingDependencies().GetEnumerator();
					while (incomingDependencies.MoveNext()) {
						sw.WriteLine(tab(4) + "<Assembly>" + ((IAssembly)incomingDependencies.Current).getAssemblyName() + "</Assembly>");
					}
					sw.WriteLine(tab(3) + "</IncomingDependencies>");
					
					sw.WriteLine(tab(2) + "</Summary>");
					sw.WriteLine(tab(1) + "</Assembly>");
				}
				sw.WriteLine("</Assemblies>");
				sw.WriteLine("</assanalyzer>");
				
			
			}
		}
		public static String tab(int x) {
			String tab = "";
			for (int i = 1; i <= x; i++) {
				tab = tab + "   ";
			}
			return tab;
			
		}

    }