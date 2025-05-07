module Skill

// Model
type Skill = { SkillName: string; Value: int }

let init name = { SkillName = name; Value = 0 }

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue of ceiling: option<int>
    | MinusOneToValue of celing: option<int>

let update (msg: Msg) (model: Skill) =

    match msg with
    | ModifyName newName -> { model with SkillName = newName }
    | AddOneToValue(Some ceiling) ->
        let valuePlusOne = model.Value + 1

        if valuePlusOne > ceiling then
            model
        else
            { model with Value = valuePlusOne }
    | AddOneToValue None -> model
    | MinusOneToValue optionalCeiling ->
        let valuePlusOne = model.Value - 1

        match optionalCeiling with
        | Some ceiling ->
            if valuePlusOne > ceiling then
                model
            else
                { model with Value = valuePlusOne }
        | None -> { model with Value = model.Value - 1 }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Skill) (dispatch: Msg -> unit) =
    [
        Daisy.input [
            color.bgPrimary
            prop.className "text-center text-xs"
            prop.value model.SkillName
            prop.onTextChange (fun newName -> dispatch (ModifyName newName))
        ]
        Html.text model.Value
        Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch (AddOneToValue None)) ]
        Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch (MinusOneToValue None)) ]
    ]
    |> List.map Html.td
    |> Html.tr