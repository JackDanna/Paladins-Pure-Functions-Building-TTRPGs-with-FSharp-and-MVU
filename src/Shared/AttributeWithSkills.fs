module AttributeWithSkills

open Skill

// Model
type AttributeWithSkills = {
    AttributeWithSkillsName: string
    Level: int
    SkillList: Skill List
}

let init (attributeWithSkillsName, skillListNames) = {
    AttributeWithSkillsName = attributeWithSkillsName
    Level = 0
    SkillList = List.map Skill.init skillListNames
}

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue
    | MinusOneToValue
    | SkillMsgAtPosition of skillMsg: Skill.Msg * position: int

let injectModelIntoMsg model msg =
    match msg with
    | SkillMsgAtPosition(Skill.AddOneToValue _, positions) ->
        SkillMsgAtPosition(Skill.AddOneToValue(Some model.Level), positions)
    | SkillMsgAtPosition(Skill.MinusOneToValue _, positions) ->
        SkillMsgAtPosition(Skill.MinusOneToValue(Some model.Level), positions)
    | _ -> msg

let update (msg: Msg) (model: AttributeWithSkills) =
    match injectModelIntoMsg model msg with
    | ModifyName newName -> {
        model with
            AttributeWithSkillsName = newName
      }
    | AddOneToValue -> { model with Level = model.Level + 1 }
    | MinusOneToValue -> { model with Level = model.Level - 1 }
    | SkillMsgAtPosition(msg: Skill.Msg, position: int) -> {
        model with
            SkillList =
                model.SkillList
                |> List.mapi (fun index skill -> if index = position then Skill.update msg skill else skill)
      }