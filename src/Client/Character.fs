module Character

open CustomizableAttribute

// Model
type Character = {
    CharacterName: string
    CustomizableAttribute: CustomizableAttribute
}

let init () = {
    CharacterName = ""
    CustomizableAttribute = CustomizableAttribute.init "Strength"
}

// Update
type Msg =
    | ModifyCharacterName of string
    | CustomizableAttributeMsg of CustomizableAttribute.Msg

let update (msg: Msg) (model: Character) =

    match msg with
    | ModifyCharacterName newName -> { model with CharacterName = newName }
    | CustomizableAttributeMsg msg -> {
        model with
            CustomizableAttribute = CustomizableAttribute.update msg model.CustomizableAttribute
      }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Character) (dispatch: Msg -> unit) =
    Html.div [
        prop.className ""
        prop.children [
            Daisy.input [
                prop.type'.text
                prop.className "input-primary bg-base-200 text-center text-2xl w-full m-4"
                prop.placeholder "Enter Character Name..."
                prop.value model.CharacterName
            ]
            CustomizableAttribute.view model.CustomizableAttribute (CustomizableAttributeMsg >> dispatch)
        ]
    ]