({
    /* Отформатировать для просмотра и показать JSON в окне  */
    onLoadJson: function (p) {
        $$('T1%id%').parse(p.json);
    },



    id: '%id%',
    rows: [
        {
            id: 'T1%id%',
            view: 'tree',
            select: true,
            borderless: true,
            type: "lineTree",
            template: m.commands._templateJson,
            on: {
                onBeforeOpen: function (id) {
                    m.commands._openJson(this, id);
                }
            }
        }
    ]
})