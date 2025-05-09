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