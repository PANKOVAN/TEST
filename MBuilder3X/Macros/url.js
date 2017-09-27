({
    ///////////////////////////////////////////////////////////////////////////
    // URL для источников данных
    // $list
    //  data              - источник данных
    //  datafetch=-1      - источник данных
    //  loadahead=-1      - источник данных
    ///////////////////////////////////////////////////////////////////////////
    $url: function (p) {
        let result = {};
        if (p.url) {
            result.url = p.url; // + "?datafetch=" + p.datafetch + "&loadahead=" + p.loadahead
            if (p.save) {
                result.save = {
                    url: p.save + (p.save.indexOf('?') > 0 ? '&' : '?') + "save=1",
                    updateFromResponse: true,
                    autoupdate: false
                };
            }
        }
        if (p && p.on) result.on = p.on;
        if (p && p.masterData) result.masterData = p.masterData;
        if (p && p.masterForm) result.masterForm = p.masterForm;
        return result;
    }
});
