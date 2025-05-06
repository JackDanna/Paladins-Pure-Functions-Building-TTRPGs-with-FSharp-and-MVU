module Character2

open CustomizableAttributeList

// Model
type Character = {
    CharacterName: string
    CustomizableAttributeList: CustomizableAttributeList
}

let init () = {
    CharacterName = ""
    CustomizableAttributeList = CustomizableAttributeList.init ()
}

// Update
type Msg =
    | ModifyCharacterName of string
    | CustomizableAttributeListMsg of CustomizableAttributeList.Msg

let update (msg: Msg) (model: Character) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | CustomizableAttributeListMsg msg -> {
        model with
            CustomizableAttributeList = CustomizableAttributeList.update msg model.CustomizableAttributeList
      }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Character) (dispatch: Msg -> unit) =
    Html.div [
        prop.className "flex flex-col items-center"
        prop.children [
            Daisy.input [
                prop.type'.text
                prop.className "input-primary bg-base-200 text-center text-2xl w-3/4 m-4"
                prop.placeholder "Enter Character Name..."
                prop.value model.CharacterName
                prop.onTextChange (ModifyCharacterName >> dispatch)
            ]
            CustomizableAttributeList.view model.CustomizableAttributeList (CustomizableAttributeListMsg >> dispatch)
        ]
    ]