({
    ///////////////////////////////////////////////////////////////////////////
    // Список элементов для form
    // $elements
    //  elements           - список колонок
    //  className         - класс
    ///////////////////////////////////////////////////////////////////////////
    $elements: function (p) {
        //m.sys.bp();
        let result = { elements: [] };
        let mclass = m.macro.getModelClass(p.className);
        // Построение как задано в параметре elements c дополнением из описателя класса
        if (p.elements) {
            result.elements = m.macro.parse(p.elements, p);
            if (mclass) {
                let l = m.sys.find(result.elements, function (val) { return val.id || val.name });
                for (let i = 0; i < l.length; i++) {
                    let id = l[i].id || l[i].name;
                    let mprop = mclass[id];
                    if (mprop && mprop._mtype == 'prop') {
                        let json = m.macro.run('$element', { id: id, prop: mprop });
                        if (!m.sys.isEmpty(json)) {
                            let el = l[i];
                            for (let k in json) {
                                if (!el[k]) el[k] = m.macro.parse(json[k], p);
                            }
                        }
                    }
                }
            }

        }
        // Построение только по описателю класса
        else if (mclass) {

            for (let n in mclass) {
                if (mclass[n]._mtype == "prop") {
                    let json = m.macro.run('$element', { id: n, prop: mclass[n] });
                    if (!m.sys.isEmpty(json)) result.elements.push(json);
                }
            }
        }
        return result;
    }
});
({
    ///////////////////////////////////////////////////////////////////////////
    // Один элемент для form
    // $element
    //  element           - одна колонка
    //  prop             - свойсва класса
    ///////////////////////////////////////////////////////////////////////////
    $element: function (p) {
        //m.sys.bp();
        let result = null;
        let prop = p.prop;
        let element = p.element;
        if ((!prop || prop._visible !== false) && (!element || element._visible !== false)) {
            result = {};
            result.name = m.sys.webixName(p.id);
            if (prop) {
                result.label = prop._header;
                result.view = 'text';
                result.labelPosition = 'top';
                if (prop._datatype == 'bool') {
                    result.view = 'checkbox'
                }
                else if (prop._datatype == 'date') {
                    result.view = 'datepicker'
                }

                for (let k in prop) {
                    if (k.substring(0, 1) != '_') {
                        result[k] = prop[k];
                    }
                }
                if (prop._element) {
                    for (let k in prop._element) {
                        result[k] = prop._element[k];
                    }
                }
            }
            if (element) {
                for (let k in element) {
                    result[k] = element[k];
                }
            }
        }
        if (result.view == 'textarea') {
            if (typeof (result.paddingY) == 'undefined') result.paddingY = 1;
        }
        return result;
    }
});
