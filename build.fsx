#r "paket:
version 7.0.2
framework: net6.0
source https://api.nuget.org/v3/index.json

nuget Microsoft.Build 17.3.2
nuget Microsoft.Build.Framework 17.3.2
nuget Microsoft.Build.Tasks.Core 17.3.2
nuget Microsoft.Build.Utilities.Core 17.3.2

nuget Be.Vlaanderen.Basisregisters.Build.Pipeline 6.0.6 //"

#load "packages/Be.Vlaanderen.Basisregisters.Build.Pipeline/Content/build-generic.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO.FileSystemOperators
open ``Build-generic``

let assemblyVersionNumber = (sprintf "%s.0")
let nugetVersionNumber = (sprintf "%s")

let buildSolution = buildSolution assemblyVersionNumber
let publishSource = publish assemblyVersionNumber
let pack = packSolution nugetVersionNumber
let test = testSolution

supportedRuntimeIdentifiers <- [ "linux-x64" ] 

// Library ------------------------------------------------------------------------
Target.create "Lib_Build" (fun _ ->
    buildSolution "basisregisters-acmidm"
)

Target.create "Test_Solution" (fun _ -> test "basisregisters-acmidm")

Target.create "Lib_Publish" (fun _ ->
    publishSource "Be.Vlaanderen.Basisregisters.AcmIdm"
)

Target.create "Lib_Pack" (fun _ -> pack "Be.Vlaanderen.Basisregisters.AcmIdm")

// --------------------------------------------------------------------------------
Target.create "PublishAll" ignore
Target.create "PackageAll" ignore

"DotNetCli"
==> "Clean"
==> "Restore"
==> "Lib_Build"
==> "Test_Solution"
==> "Lib_Publish"
==> "PublishAll"

"PublishAll"
==> "Lib_Pack"
==> "PackageAll"

Target.runOrDefault "Test_Solution"
