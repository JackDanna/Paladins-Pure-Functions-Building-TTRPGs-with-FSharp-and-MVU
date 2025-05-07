module Character

open AttributeList

// Model
type Character = {
    CharacterName: string
    AttributeList: AttributeList
}

let init attributeNameList = {
    CharacterName = ""
    AttributeList = AttributeList.init attributeNameList
}

// Update
type Msg =
    | ModifyCharacterName of string
    | AttributeListMsg of AttributeList.Msg

let update (msg: Msg) (model: Character) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | AttributeListMsg msg -> {
        model with
            AttributeList = AttributeList.update msg model.AttributeList
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
            AttributeList.view model.AttributeList (AttributeListMsg >> dispatch)
        ]
    ]