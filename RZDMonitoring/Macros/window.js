({
    ///////////////////////////////////////////////////////////////////////////
    // window      - диалоговое окно
    ///////////////////////////////////////////////////////////////////////////
    $window: function (p) {
        return {
            view: 'window',
            modal: true,
            move: true,
            resize: true,
            position: function (state) {
                state.left = 100;
                state.top = 100;
                state.width = state.maxWidth - 200;
                state.height = state.maxHeight - 200;
            },
            head: {
                $pageBar: { name: p.name },
            },
            body: p.content
        }
    }
});
