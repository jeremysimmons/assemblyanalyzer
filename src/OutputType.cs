using System;

class OutputType {
	private String file;
	private String type;
	
	public OutputType(String file, String type) {
		this.file = file;
		this.type = type;
	}
	
	public String getFile() { return this.file; }
	
	public String getOutputType() { return this.type; }
}