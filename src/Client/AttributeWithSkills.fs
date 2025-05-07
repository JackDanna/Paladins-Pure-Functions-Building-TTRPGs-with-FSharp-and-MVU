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

let update (msg: Msg) (model: AttributeWithSkills) =
    match msg with
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
    Daisy.table [
        Html.thead [
            [
                Daisy.input [
                    color.bgPrimary
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