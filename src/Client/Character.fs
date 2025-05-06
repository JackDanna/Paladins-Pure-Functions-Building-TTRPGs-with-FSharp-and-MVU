module Character

open Attribute

// Model
type Character = {
    CharacterName: string
    CustomizableAttribute: Attribute
}

let init () = {
    CharacterName = ""
    CustomizableAttribute = Attribute.init "Strength"
}

// Update
type Msg =
    | ModifyCharacterName of string
    | CustomizableAttributeMsg of Attribute.Msg

let update (msg: Msg) (model: Character) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | CustomizableAttributeMsg msg -> {
        model with
            CustomizableAttribute = Attribute.update msg model.CustomizableAttribute
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
            Attribute.view model.CustomizableAttribute (CustomizableAttributeMsg >> dispatch)
        ]
    ]