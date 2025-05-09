module Character

open AttributeWithSkillsList

// Model
type Character = {
    CharacterName: string
    AttributeWithSkillsList: AttributeWithSkillsList
}

let init characterName attributeNameList = {
    CharacterName = characterName
    AttributeWithSkillsList = AttributeWithSkillsList.init attributeNameList
}

// Update
type Msg =
    | ModifyCharacterName of string
    | AttributeListMsg of AttributeWithSkillsList.Msg

let update (msg: Msg) (model: Character) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | AttributeListMsg msg -> {
        model with
            AttributeWithSkillsList = AttributeWithSkillsList.update msg model.AttributeWithSkillsList
      }