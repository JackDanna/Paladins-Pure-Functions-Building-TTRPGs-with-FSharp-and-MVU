module CustomizableAttributeList

open CustomizableAttribute

// Model
type CustomizableAttributeList = CustomizableAttribute List

let init () =
    List.map CustomizableAttribute.init [ "Strength"; "Agility"; "Intelligence"; "Charisma" ]

// Update
type Msg = AttributeMsgAtPosition of attributeMsg: CustomizableAttribute.Msg * position: int

let update (msg: Msg) (model: CustomizableAttributeList) =

    match msg with
    | AttributeMsgAtPosition(msg: CustomizableAttribute.Msg, position: int) ->
        model
        |> List.mapi (fun index attribute ->
            if index = position then
                CustomizableAttribute.update msg attribute
            else
                attribute)

// View
open Feliz
open Feliz.DaisyUI

let view (model: CustomizableAttributeList) (dispatch: Msg -> unit) =

    Daisy.card [
        prop.className "flex items-center"
        prop.children (
            model
            |> List.mapi (fun index attribute ->
                CustomizableAttribute.view attribute (fun msg -> AttributeMsgAtPosition(msg, index) |> dispatch))
        )
    ]