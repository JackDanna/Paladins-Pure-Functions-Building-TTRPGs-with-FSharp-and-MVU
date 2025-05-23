module Skill

// Model
type Skill = { SkillName: string; Value: int }

let init name = { SkillName = name; Value = 0 }

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue of governingAttributeLevel: option<int>
    | MinusOneToValue of governingAttributeLevel: option<int>

let update (msg: Msg) (model: Skill) =

    match msg with
    | ModifyName newName -> { model with SkillName = newName }
    | AddOneToValue(Some governingAttributeLevel) ->
        let valuePlusOne = model.Value + 1

        if valuePlusOne > governingAttributeLevel then
            model
        else
            { model with Value = valuePlusOne }
    | AddOneToValue None -> model
    | MinusOneToValue optionalCeiling ->
        let valuePlusOne = model.Value - 1

        match optionalCeiling with
        | Some governingAttributeLevel ->
            if valuePlusOne > governingAttributeLevel then
                model
            else
                { model with Value = valuePlusOne }
        | None -> { model with Value = model.Value - 1 }