with import <nixpkgs> {};

mkShell {
  name = "dotnet-env";
  packages = [
    dotnetCorePackages.sdk_8_0
	sqlcmd
  ];
}
