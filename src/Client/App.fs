module App

open Elmish
open Elmish.React
open Elmish.Bridge

open Fable.Core.JsInterop

importSideEffects "./index.css"

#if DEBUG
open Elmish.HMR
#endif

Program.mkProgram Index.init Index.update Index.view
|> Program.withBridgeConfig (Bridge.endpoint Shared.Bridge.endpoint |> Bridge.withMapping Index.Msg.RC)
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactSynchronous "elmish-app"

|> Program.run