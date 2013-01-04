using System;
using System.Collections;
using Mono.Cecil;

class CompleteAssembly : IAssembly {
	AssemblyDefinition assemblyDfn;
	IList incomingDependencies;
	IList outgoingDependencies;
	
	public CompleteAssembly(AssemblyDefinition assemblyDfn) {
		this.assemblyDfn = assemblyDfn;
		this.incomingDependencies = new ArrayList();
		this.outgoingDependencies = new ArrayList();
	}
	public IList getAllContainedPackages() {return null;}
    //public abstract List getAllExternallyReferencedPackages();

    public int getPackageCount() {return 0; }

	public String getAssemblyName() { return this.assemblyDfn.Name.Name.Replace(".","_"); }

    public IList getOutgoingDependencies() { 

		ArrayList assemblies = new ArrayList();
		foreach (AssemblyNameReference type in this.assemblyDfn.MainModule.AssemblyReferences)
        {
            //Writes the full name of a type
            //sw.WriteLine(dfn.Name.Name.Replace(".", "_") + " -> " + type.Name.Replace(".","_"));
			assemblies.Add(type.Name.Replace(".","_"));
        }

		return assemblies; 
	}
	
	public IList getAnalyzedOutgoingDependencies() {
		return this.outgoingDependencies;
	}
	
	public IList getAnalyzedIncomingDependencies() {
		return this.incomingDependencies;
	}
	
	public void addAnalyzedOutgoingDependency(IAssembly a) {
		this.outgoingDependencies.Add(a);
	}
	public void addAnalyzedIncomingDependency(IAssembly a) {
		this.incomingDependencies.Add(a);
	}
	
    public IList getIncomingDependencies() { return null; }
	public Metrics calculateMetrics() { 
		return new Metrics(this); 
	}
	
	public int getClassCount() { 
		int i = 0;
		foreach(TypeDefinition type in assemblyDfn.MainModule.Types) {
			if (type.Name != "<Module>") {
				//Console.WriteLine(type.Name);
				i ++;
			}
		}
		return i; 
	}
	
	public int getAbstractClassCount() { 
		int i = 0;
		foreach(TypeDefinition type in assemblyDfn.MainModule.Types) {
			if (type.IsAbstract || type.IsInterface) {
				if (type.Name != "<Module>") {
					//Console.WriteLine("ABSTRACT --> " + type.Name);
					i ++;
				}
			}
		}
		return i;
	}
}