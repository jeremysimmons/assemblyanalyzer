using System;
using System.Collections;
using Mono.Cecil;

class AssemblyBundle {
	IList assemblies;
	
	public AssemblyBundle(IList assemblyList) {
		this.assemblies = new ArrayList();
		IEnumerator iAssemblies = assemblyList.GetEnumerator();
		while (iAssemblies.MoveNext()) {
			AssemblyDefinition dfn = AssemblyFactory.GetAssembly((String)iAssemblies.Current);
			IAssembly assembly = new CompleteAssembly(dfn);
			//Console.WriteLine("ASSEMBLY: " + assembly.getAssemblyName() + "  " + dfn.GetType());
			this.assemblies.Add(assembly);
		}
		this.buildRelationships();
	}
	
	
	private void buildRelationships() {	
		IEnumerator allAssemblies = this.assemblies.GetEnumerator();
		while (allAssemblies.MoveNext()) {
			IAssembly assembly = (IAssembly)allAssemblies.Current;
			IEnumerator outgoingDependencies = assembly.getOutgoingDependencies().GetEnumerator();
			while (outgoingDependencies.MoveNext()) {
				String assemblyName = (String)outgoingDependencies.Current;
				IAssembly dependentAssembly = this.getAnalyzedAssembly(assemblyName);
				if (dependentAssembly != null) {
					dependentAssembly.addAnalyzedIncomingDependency(assembly);
					assembly.addAnalyzedOutgoingDependency(dependentAssembly);
				}
			}
		}
	}
	
	private IAssembly getAnalyzedAssembly(String assemblyName) {
		IEnumerator allAssemblies = this.assemblies.GetEnumerator();
		while (allAssemblies.MoveNext()) {
			IAssembly assembly = (IAssembly)allAssemblies.Current;
			if (assemblyName == assembly.getAssemblyName()) {
				return assembly;
			} 
		}
		return null;
	}
	
	public IEnumerator getEnum() { return this.assemblies.GetEnumerator(); }
}