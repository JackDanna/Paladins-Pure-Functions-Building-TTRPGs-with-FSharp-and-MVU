module SimpleIndex

open Elmish
open SAFE
open Shared

open AttributeList

type Model = { AttributeList: AttributeList }

type Msg = AttributeListMsg of AttributeList.Msg

let init () = {
    AttributeList = AttributeList.init [ "Strength"; "Reflex"; "Intelligence"; "Charisma;" ]
}

let update msg model =
    match msg with
    | AttributeListMsg msg -> {
        model with
            AttributeList = AttributeList.update msg model.AttributeList
      }

open Feliz

let view model dispatch =
    Html.section [
        prop.className "h-screen w-screen relative overflow-hidden"
        prop.children [
            // Meta viewport tag for proper mobile scaling
            Html.meta [
                prop.name "viewport"
                prop.content "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"
            ]

            // Background div with image and glass effect
            // Html.div [
            //     prop.className
            //         "absolute inset-0 bg-cover bg-center bg-fixed bg-no-repeat
            //     bg-white/20 backdrop-blur-sm"
            //     prop.style [ style.backgroundImageUrl "https://unsplash.it/1200/900?random" ]
            // ]

            // Content container (the rest of your UI)
            Html.div [
                prop.className "relative z-10 h-full w-full"
                prop.children [
                    // Your existing content here
                    Html.a [
                        prop.href "https://safe-stack.github.io/"
                        prop.className
                            "absolute block ml-4 sm:ml-12 h-10 w-10 sm:h-12 sm:w-12 bg-teal-300 hover:cursor-pointer hover:bg-teal-400"
                        prop.children [ Html.img [ prop.src "/favicon.png"; prop.alt "Logo" ] ]
                    ]

                    Html.div [
                        prop.className "flex flex-col items-center justify-center h-full"
                        prop.children [
                            Html.div [
                                prop.className
                                    "bg-white/20 backdrop-blur-lg p-4 sm:p-8 rounded-xl shadow-lg border border-white/30 mx-4 sm:mx-0 max-w-full sm:max-w-2xl"
                                prop.children [
                                    AttributeList.view model.AttributeList (AttributeListMsg >> dispatch)
                                    Html.h1 [
                                        prop.className "text-center text-3xl sm:text-5xl font-bold mb-3 p-2 sm:p-4"
                                        prop.text "Paladins_Pure_Functions_Building_TTRPGs_with_FSharp_and_MVU"
                                    ]
                                //ViewComponents.todoList model dispatch
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]