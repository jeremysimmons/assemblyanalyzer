using System;
using System.Collections;

interface IAssembly {
	
	
	IList getAllContainedPackages();
    //public abstract List getAllExternallyReferencedPackages();

    int getPackageCount();
    int getAbstractClassCount();
    int getClassCount();
	String getAssemblyName();
    IList getOutgoingDependencies();
	void addAnalyzedIncomingDependency(IAssembly a);
	void addAnalyzedOutgoingDependency(IAssembly a);
	IList getAnalyzedOutgoingDependencies();
	IList getAnalyzedIncomingDependencies();
	
    IList getIncomingDependencies();
	Metrics calculateMetrics();
}