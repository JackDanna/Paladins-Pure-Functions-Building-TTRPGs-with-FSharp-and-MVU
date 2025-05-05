
{
  description = "A FSharp Dev Shell";

  inputs = {
    # nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    nixpkgs-old.url = "github:NixOS/nixpkgs/c792c60b8a97daa7efe41a6e4954497ae410e0c1";
  };

  outputs = { self, nixpkgs, nixpkgs-old }:
  let
    system = "x86_64-linux";
    pkgs = import nixpkgs {
      inherit system;
      config = {
        allowUnfreePredicate = pkg: builtins.elem (pkgs.lib.getName pkg) [
          "vscode-with-extensions"
          "vscode"
          "vscode-extension-mhutchie-git-graph"
        ];
      };
    };
    
    oldPkgs = import nixpkgs-old {
      inherit system;
    };
  in 
  {
    packages.${system} = {
      default = pkgs.writeShellScriptBin "run" ''
        nix develop -c -- code .
      '';
    };

    devShells.${system}.default = pkgs.mkShell rec {
      name = "FSharpDevShell";
      buildInputs = with pkgs; [
        bashInteractive
        dotnet-sdk_8
        nodejs_20
        (vscode-with-extensions.override  {
          vscode = pkgs.vscode;
          vscodeExtensions = with pkgs.vscode-extensions; [
            ionide.ionide-fsharp
            oldPkgs.vscode-extensions.ms-dotnettools.csharp # We need to make sure we use version 2.39.32 since there is a bug otherise: https://github.com/ionide/ionide-vscode-fsharp/issues/2039
            ms-dotnettools.vscode-dotnet-runtime

            jnoortheen.nix-ide
            mhutchie.git-graph
          ] ++ pkgs.vscode-utils.extensionsFromVscodeMarketplace [
            # {
            #   name = "vscode-dotnet-pack";
            #   publisher = "ms-dotnettools";
            #   version = "1.0.13";
            #   sha256 = "sha256-z3xiXgWADSHdZM/+MSmqRXqDjiX4O6whevN1zSmByWQ=";
            # }
          ];
        })
      ];

      shellHook = ''
        export PS1+="${name}> "
        echo "Welcome to the FSharp Dev Shell!"
      '';
    };
  };

}

