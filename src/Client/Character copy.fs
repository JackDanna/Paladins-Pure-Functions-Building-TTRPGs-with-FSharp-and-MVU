module Character

open CustomizableAttribute

// Model
type Character = {
    CharacterName: string
    CharacterArtURL: string
    CustomizableAttribute: CustomizableAttribute
}

let init () = {
    CharacterName = ""
    CharacterArtURL = ""
    CustomizableAttribute = CustomizableAttribute.init "Strength"
}

// Update
type Msg =
    | ModifyCharacterName of string
    | ModifyCharacterArtURL of string
    | CustomizableAttributeMsg of CustomizableAttribute.Msg

let update (msg: Msg) (model: Character) =

    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | ModifyCharacterArtURL newCharacterArtURL -> {
        model with
            CharacterArtURL = newCharacterArtURL
      }
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
                prop.onTextChange (ModifyCharacterName >> dispatch)
            ]
            Html.div [
                prop.className "flex flex-col items-center"
                prop.children [
                    Html.img [ prop.src model.CharacterArtURL; prop.style [ style.width 800 ] ]
                    Daisy.input [
                        prop.type'.text
                        prop.placeholder "Enter Character Art URL..."
                        prop.value model.CharacterArtURL
                        prop.onTextChange (ModifyCharacterArtURL >> dispatch)
                    ]
                ]
            ]
            CustomizableAttribute.view model.CustomizableAttribute (CustomizableAttributeMsg >> dispatch)
        ]
    ]