module App

open Elmish
open Elmish.React

open Fable.Core.JsInterop

importSideEffects "./index.css"

#if DEBUG
open Elmish.HMR
#endif

// Program.mkProgram Index.init Index.update Index.view
//Program.mkSimple SimpleIndex.init SimpleIndex.update SimpleIndex.view
Program.mkSimple Character2.init Character2.update Character2.view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactSynchronous "elmish-app"

|> Program.run