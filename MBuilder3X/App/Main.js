(
    {
        onLoadJson: function (p) { m.main.runCommand('start') },
        view: "layout",
        rows: [
            {
                $appJson: {
                    name: 'MainCommands'
                }
            },
            {
                "id": "mainviews",
                "view": "multiview",
                "keepViews": true,
                "cells": [
                    { "template": "..." }
                ],
                "autoheight": true,
                "autowidth": true
            }
        ]
    }
)
