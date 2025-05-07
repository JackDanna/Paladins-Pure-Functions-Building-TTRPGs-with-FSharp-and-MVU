module Character2

open AttributeWithSkillsList

// Model
type Character2 = {
    CharacterName: string
    AttributeWithSkillsList: AttributeWithSkillsList
}

let init characterName attributeNameList = {
    CharacterName = characterName
    AttributeWithSkillsList = AttributeWithSkillsList.init attributeNameList
}

// Update
type Msg =
    | ModifyCharacterName of string
    | AttributeListMsg of AttributeWithSkillsList.Msg

let update (msg: Msg) (model: Character2) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | AttributeListMsg msg -> {
        model with
            AttributeWithSkillsList = AttributeWithSkillsList.update msg model.AttributeWithSkillsList
      }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Character2) (dispatch: Msg -> unit) =
    Html.div [
        prop.className "flex flex-col items-center"
        prop.children [
            Daisy.input [
                prop.type'.text
                prop.className "input-primary bg-base-200 text-center text-2xl w-3/4 m-4"
                prop.placeholder "Enter Character2 Name..."
                prop.value model.CharacterName
                prop.onTextChange (ModifyCharacterName >> dispatch)
            ]
            AttributeWithSkillsList.view model.AttributeWithSkillsList (AttributeListMsg >> dispatch)
        ]
    ]