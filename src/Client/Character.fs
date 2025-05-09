module Character

open Character

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
            Html.img [ prop.src model.CharacterArtUrl; prop.style [ style.width 800 ] ]
            Daisy.input [
                prop.value model.CharacterArtUrl
                prop.className "text-center"
                prop.placeholder "Enter Character Art URL"
                prop.onTextChange (ModifyCharacterUrl >> dispatch)
            ]

            AttributeWithSkillsList.view model.AttributeWithSkillsList (AttributeListMsg >> dispatch)
        ]
    ]