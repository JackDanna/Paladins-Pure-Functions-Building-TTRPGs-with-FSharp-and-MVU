module AttributeWithSkillsList

open AttributeWithSkillsList


// View
open Feliz

let view (model: AttributeWithSkillsList) (dispatch: Msg -> unit) =
    Html.div [
        prop.className "flex flex-row"
        model
        |> List.mapi (fun index attributeWithSkills ->

            let dispatchForAttributeAtPosition (msg: AttributeWithSkills.Msg) =
                dispatch (AttributeWithSkillsMsgAtPosition(msg, index))

            AttributeWithSkills.view attributeWithSkills dispatchForAttributeAtPosition)
        |> prop.children
    ]

//AttributeWithSkills.view attributeWithSkills (fun msg -> AttributeMsgAtPosition(msg, index) |> dispatch)