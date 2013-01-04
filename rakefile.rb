task :compile do
  sh "mkdir ./bin; mcs -reference:./lib/Mono.Cecil.dll -out:./bin/analyze.exe ./src/*.cs"
  cp "./src/assanalyzerconfig.xml", "./bin/assanalyzerconfig.xml"
  cp "./lib/Mono.Cecil.dll", "./bin/Mono.Cecil.dll"
  cp "./license.txt", "./bin/license.txt"
  cp "./scripts/assanalyzer.xsl", "./bin/assanalyzer.xsl"
  #File.copy("./src/assanalyzerconfig.xml", "./bin/assanalyzerconfig.xml")
  #File.copy("./lib/Mono.Cecil.dll", "./bin/Mono.Cecil.dll")
  #File.copy("./license.txt", "./bin/license.txt")
  #File.copy("./scripts/assanalyzer.xsl", "./bin/assanalyzer.xsl")
end

task :run do
  sh "mkdir ./out"
  #File.copy("./style/index.html", "./out/index.html")
  cp "./style/index.html", "./out/index.html"
  sh "cd ./bin; mono analyze.exe"
end

task :rundot do
  sh "/Applications/Graphviz.app/Contents/MacOS/dot -Tpng -Nshape=box -Nfontsize=30 -Nwidth=1.5 -Nheight=1.25 ./out/out.grph -o ./out/out.png"
end

task :rundotall do
  sh "/Applications/Graphviz.app/Contents/MacOS/dot -Tpng -Nshape=box -Nfontsize=30 -Nwidth=1.5 -Nheight=1.25 ./out/outall.grph -o ./out/outall.png"
end

task :style do
  sh "ant -f ./scripts/style.xml"
end

task :move do
  if ENV['CC_BUILD_ARTIFACTS']
    dir = ENV['CC_BUILD_ARTIFACTS']
    File.move("./out", dir + "/out")
  end
  #File.move("./out/out.grph", dir + "/out.grph")
  #Dir.mkdir(dir + "/out")
  #File.move
end

task :clean do
  sh "cd ./out; rm *.html; rm *.png; rm *.xml; rm *.grph"
  sh "cd ./bin; rm *"
  sh "rmdir ./bin"
  sh "rmdir ./out"
end

task :runall => [:run, :rundot, :rundotall, :style]

task :cruise => [:compile, :runall, :move]