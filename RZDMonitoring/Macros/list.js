({
    ///////////////////////////////////////////////////////////////////////////
    // Список с пагинатором
    // $list
    //  id              - id
    //  data            - источник данных
    //  page            - размер страницы для пагинатора
    //  template        - шаблон
    ///////////////////////////////////////////////////////////////////////////
    $list: function (p) {
        return {
            rows: [
                {
                    id: p.id,
                    view: "list",
                    select: 'multiselect',
                    $content: function (p) {
                        if (p.page) return {
                            pager: "P" + p._uid_
                        }
                    },
                    borderless: true,
                    type: {
                        height: "auto",
                        width: "auto"
                    },
                    $url: {},
                    template: p.template,
                    onClick: p.onClick
                }
                /*,

                {
                    $content: function (p) {
                        if (p.page) return {
                            view: "pager",
                            id: "P" + p._uid_,
                            template: "{common.first()}{common.prev()}{common.pages()}{common.next()}{common.last()} <span class=\"text-muted\">Страница {common.page()} из #limit#</span>",
                            group: 5,
                            size: p.page,
                            on: {
                                onBeforePageChange: function (id, e, node) {
                                    let l = $$(p.id);
                                    l.clearAll();
                                    l.load(l.url + '&start=' + id * p.page + '&count=' + p.page);
                                }
                            }
                        }
                    }
                }
                */
            ]
        }
    }
});
