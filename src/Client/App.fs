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
// Program.mkSimple
//     (fun () -> Character.init [ "Strength"; "Agility"; "Intelligence"; "Charisma" ])
//     Character.update
//     Character.view
Program.mkSimple
    (fun () ->
        Character2.init "" [
            "Strength", [ "Athletics"; "Lift"; "Endurance" ]
            "Agility", [ "Acrobatics"; "Slieght Of Hand"; "Stealth" ]
            "Intelligence", [ "History"; "Arcana"; "Survival" ]
        ])
    Character2.update
    Character2.view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactSynchronous "elmish-app"

|> Program.run