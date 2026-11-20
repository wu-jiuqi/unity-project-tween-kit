using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

// Syntax inventory deliberately does not claim that a textual reference proves reachability.
static class SourceInventory {
 static string Hash(byte[] b)=>Convert.ToHexString(SHA256.HashData(b)).ToLowerInvariant();
 static string Owner(SyntaxNode n)=>string.Join(".",n.Ancestors().OfType<TypeDeclarationSyntax>().Reverse().Select(t=>t.Identifier.ValueText));
 static string Member(SyntaxNode n)=>n is BaseMethodDeclarationSyntax m?(m is MethodDeclarationSyntax f?f.Identifier.ValueText:m is ConstructorDeclarationSyntax c?c.Identifier.ValueText:"operator"):n is PropertyDeclarationSyntax p?p.Identifier.ValueText:n is FieldDeclarationSyntax d?string.Join(",",d.Declaration.Variables.Select(v=>v.Identifier.ValueText)):"";
 public static void Main(string[] args){
  string root=Path.GetFullPath(args[0]);var all=new List<object>();
  var paths=Directory.GetFiles(Path.Combine(root,"Assets"),"*.cs",SearchOption.AllDirectories).Where(path=>path.Contains(Path.DirectorySeparatorChar+"Scripts"+Path.DirectorySeparatorChar)||path.Contains(Path.DirectorySeparatorChar+"Editor"+Path.DirectorySeparatorChar)).OrderBy(x=>x).ToArray();
  var trees=paths.ToDictionary(p=>p,p=>CSharpSyntaxTree.ParseText(File.ReadAllText(p),new CSharpParseOptions(preprocessorSymbols:new[]{"UNITY_STANDALONE","UNITY_STANDALONE_WIN","UNITY_6000_0_OR_NEWER"}),p));
  var refs=File.ReadAllLines(args[2]).Where(File.Exists).Distinct().Select(p=>MetadataReference.CreateFromFile(p));
  var compilation=CSharpCompilation.Create("SourceInventoryAnalysis",trees.Values,refs,new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
  foreach(string path in paths){
   var bytes=File.ReadAllBytes(path);var tree=trees[path];var syntax=tree.GetRoot();var model=compilation.GetSemanticModel(tree);
   var members=syntax.DescendantNodes().Where(n=>n is BaseMethodDeclarationSyntax||n is PropertyDeclarationSyntax||n is FieldDeclarationSyntax).Select(n=>new {
    owner=Owner(n),name=Member(n),kind=n.Kind().ToString(),line=tree.GetLineSpan(n.Span).StartLinePosition.Line+1,endLine=tree.GetLineSpan(n.Span).EndLinePosition.Line+1,
    sha256=Hash(Encoding.UTF8.GetBytes(n.ToFullString())),source=n.ToFullString(),symbol=(n is FieldDeclarationSyntax field?model.GetDeclaredSymbol(field.Declaration.Variables.First()):model.GetDeclaredSymbol(n))?.ToDisplayString(),
    calls=n.DescendantNodes().OfType<InvocationExpressionSyntax>().Select(i=>{var symbol=model.GetSymbolInfo(i).Symbol as IMethodSymbol;var location=symbol?.OriginalDefinition.Locations.FirstOrDefault(l=>l.IsInSource);return new{expression=i.Expression.ToString(),arguments=i.ArgumentList.ToString(),line=tree.GetLineSpan(i.Span).StartLinePosition.Line+1,callee=symbol?.OriginalDefinition.ToDisplayString(),resolution=symbol!=null?"semantic":"unresolved",sourcePath=location==null?null:Path.GetRelativePath(root,location.SourceTree.FilePath).Replace('\\','/'),sourceLine=location?.GetLineSpan().StartLinePosition.Line+1};}).ToArray(),
    types=n.DescendantNodes().OfType<GenericNameSyntax>().Where(g=>new[]{"AddComponent","GetComponent","GetComponentInChildren","GetComponentsInChildren","FindFirstObjectByType"}.Contains(g.Identifier.ValueText)).Select(g=>new{name=g.Identifier.ValueText,type=g.TypeArgumentList.ToString(),line=tree.GetLineSpan(g.Span).StartLinePosition.Line+1}).ToArray()
   }).ToArray();
   all.Add(new{path=Path.GetRelativePath(root,path).Replace('\\','/'),sha256=Hash(bytes),classes=syntax.DescendantNodes().OfType<TypeDeclarationSyntax>().Select(t=>new{name=t.Identifier.ValueText,bases=t.BaseList?.ToString()??"",line=tree.GetLineSpan(t.Span).StartLinePosition.Line+1}).ToArray(),members,
    disabled=syntax.DescendantTrivia(descendIntoTrivia:true).Where(t=>t.IsKind(SyntaxKind.DisabledTextTrivia)).Select(t=>new{line=tree.GetLineSpan(t.Span).StartLinePosition.Line+1,source=t.ToFullString()}).ToArray()});
  }
  File.WriteAllText(args[1],JsonSerializer.Serialize(all,new JsonSerializerOptions{WriteIndented=true}));
  Console.WriteLine("Inventoried "+all.Count+" C# source files");
 }
}
