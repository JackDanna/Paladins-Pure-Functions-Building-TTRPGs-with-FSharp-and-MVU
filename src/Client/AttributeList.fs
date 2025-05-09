module AttributeList

open AttributeList

// View
open Feliz
open Feliz.DaisyUI

let view (model: AttributeList) (dispatch: Msg -> unit) =
    Daisy.card [
        prop.className "flex items-center"
        prop.children (
            model
            |> List.mapi (fun index attribute ->

                let dispatchForAttributeAtPosition (msg: Attribute.Msg) =
                    dispatch (AttributeMsgAtPosition(msg, index))

                Attribute.view attribute dispatchForAttributeAtPosition)
        )
    ]

//Attribute.view attribute (fun msg -> AttributeMsgAtPosition(msg, index) |> dispatch)