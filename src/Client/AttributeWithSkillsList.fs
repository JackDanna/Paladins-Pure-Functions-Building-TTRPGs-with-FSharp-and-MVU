module AttributeWithSkillsList

open AttributeWithSkills

// Model
type AttributeWithSkillsList = AttributeWithSkills List

let init (attributeNameAndSkillNamesTuples: list<string * list<string>>) =
    List.map AttributeWithSkills.init attributeNameAndSkillNamesTuples

// Update
type Msg = AttributeWithSkillsMsgAtPosition of attributeMsg: AttributeWithSkills.Msg * position: int

let update (msg: Msg) (model: AttributeWithSkillsList) =
    match msg with
    | AttributeWithSkillsMsgAtPosition(msg: AttributeWithSkills.Msg, position: int) ->
        model
        |> List.mapi (fun index attributeWithSkills ->
            if index = position then
                AttributeWithSkills.update msg attributeWithSkills
            else
                attributeWithSkills)

// View
open Feliz
open Feliz.DaisyUI

let view (model: AttributeWithSkillsList) (dispatch: Msg -> unit) =
    Daisy.card [
        prop.className "flex items-center"
        prop.children (
            model
            |> List.mapi (fun index attributeWithSkills ->

                let dispatchForAttributeAtPosition (msg: AttributeWithSkills.Msg) =
                    dispatch (AttributeWithSkillsMsgAtPosition(msg, index))

                AttributeWithSkills.view attributeWithSkills dispatchForAttributeAtPosition)
        )
    ]

//AttributeWithSkills.view attributeWithSkills (fun msg -> AttributeMsgAtPosition(msg, index) |> dispatch)