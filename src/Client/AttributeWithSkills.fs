module AttributeWithSkills

open SkillList

// Model
type AttributeWithSkills = {
    AttributeWithSkillsName: string
    Value: int
    SkillList: SkillList
}

let init (attributeWithSkillsName, skillListNames) = {
    AttributeWithSkillsName = attributeWithSkillsName
    Value = 0
    SkillList = SkillList.init skillListNames
}

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue
    | MinusOneToValue
    | SkillListMsg of SkillList.Msg

let injectModelIntoMsg model msg =
    match msg with
    | SkillListMsg(SkillMsgAtPosition(Skill.AddOneToValue _, positions)) ->
        SkillListMsg(SkillMsgAtPosition(Skill.AddOneToValue(Some model.Value), positions))
    | SkillListMsg(SkillMsgAtPosition(Skill.MinusOneToValue _, positions)) ->
        SkillListMsg(SkillMsgAtPosition(Skill.MinusOneToValue(Some model.Value), positions))
    | _ -> msg

let update (msg: Msg) (model: AttributeWithSkills) =
    match injectModelIntoMsg model msg with
    | ModifyName newName -> {
        model with
            AttributeWithSkillsName = newName
      }
    | AddOneToValue -> { model with Value = model.Value + 1 }
    | MinusOneToValue -> { model with Value = model.Value - 1 }
    | SkillListMsg msg -> {
        model with
            SkillList = SkillList.update msg model.SkillList
      }

// View
open Feliz
open Feliz.DaisyUI

let view (model: AttributeWithSkills) (dispatch: Msg -> unit) =
    Daisy.card [
        card.border
        color.bgBase200
        prop.className "flex items-center m-4"
        Daisy.table [
            Html.thead [
                [
                    Daisy.input [
                        color.bgPrimary
                        prop.className "text-center text-white"
                        prop.value model.AttributeWithSkillsName
                        prop.onTextChange (fun newName -> dispatch (ModifyName newName))
                    ]
                    Html.text model.Value
                    Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch AddOneToValue) ]
                    Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch MinusOneToValue) ]
                ]
                |> List.map Html.th
                |> Html.tr
            ]

            Html.tbody (SkillList.view model.SkillList (SkillListMsg >> dispatch))
        ]
        |> prop.children
    ]