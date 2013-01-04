task :compile do
  sh "mcs -reference:../bin/Mono.Cecil.dll -out:../bin/analyze.exe ../src/*.cs"
end

task :run do
  sh "cd ../bin; mono analyze.exe"
end

task :rundot do
  sh "/Applications/Graphviz.app/Contents/MacOS/dot -Tpng -Nshape=box -Nfontsize=30 -Nwidth=1.5 -Nheight=1.25 ../out/out.grph -o ../out/out.png"
end

task :rundotall do
  sh "/Applications/Graphviz.app/Contents/MacOS/dot -Tpng -Nshape=box -Nfontsize=30 -Nwidth=1.5 -Nheight=1.25 ../out/outall.grph -o ../out/outall.png"
end

task :style do
  sh "ant -f style.xml"
end

task :clean do
  sh "cd ../out; rm *.html; rm *.png; rm *.xml; rm *.grph"
end

task :runall => [:run, :rundot, :rundotall, :style]

task :cruise => [:compile]