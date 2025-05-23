module AttributeList

open Attribute

// Model
type AttributeList = Attribute List

let init attributeNameList =
    List.map Attribute.init attributeNameList

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