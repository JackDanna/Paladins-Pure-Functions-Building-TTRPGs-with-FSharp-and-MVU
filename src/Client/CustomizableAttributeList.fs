module AttributeList

open Attribute

// Model
type AttributeList = Attribute List

let init () =
    List.map Attribute.init [ "Strength"; "Agility"; "Intelligence"; "Charisma" ]

// Update
type Msg = AttributeMsgAtPosition of attributeMsg: Attribute.Msg * position: int

let update (msg: Msg) (model: AttributeList) =

    match msg with
    | AttributeMsgAtPosition(msg: Attribute.Msg, position: int) ->
        model
        |> List.mapi (fun index attribute ->
            if index = position then
                Attribute.update msg attribute
            else
                attribute)

// View
open Feliz
open Feliz.DaisyUI

let view (model: AttributeList) (dispatch: Msg -> unit) =

    Daisy.card [
        prop.className "flex items-center"
        prop.children (
            model
            |> List.mapi (fun index attribute ->
                Attribute.view attribute (fun msg -> AttributeMsgAtPosition(msg, index) |> dispatch))
        )
    ]