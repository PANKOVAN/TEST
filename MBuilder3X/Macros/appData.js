({
    // $appData     - загрузка данных (используется для дальнейшей интерпретации json webix)
    // $data
    //  url   - url 11111111111
    $appData: function (p) {
        let json = m.main.getAppJson(p.name, {}, p.folder);
        return { data: json };
    }
});
