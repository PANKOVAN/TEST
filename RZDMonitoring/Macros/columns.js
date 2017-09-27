({
    ///////////////////////////////////////////////////////////////////////////
    // Список колонок для dataTable, treeTable и т.д.
    // $columns
    //  columns           - список колонок
    //  className         - класс
    ///////////////////////////////////////////////////////////////////////////
    $columns: function (p) {
        //m.sys.bp();
        let result = { columns: [] };
        let columns = result.columns;
        let mclass = m.macro.getModelClass(p.className);

        if (p.columns && mclass) {
            for (let n in p.columns) {
                let id = p.columns[n].id || p.columns[n].name;
                let json = m.macro.run('$column', { id: n, column: p.columns[n], prop: mclass[id] });
                if (!m.sys.isEmpty(json)) columns.push(json);
            }
        }
        else if (p.columns) {
            for (let n in p.columns) {
                let json = m.macro.run('$column', { id: n, column: p.columns[n] });
                if (!m.sys.isEmpty(json)) columns.push(json);
            }
        }
        else if (mclass) {
            for (let n in mclass) {
                if (mclass[n]._mtype == "prop") {
                    let json = m.macro.run('$column', { id: n, prop: mclass[n] });
                    if (!m.sys.isEmpty(json)) columns.push(json);
                }
            }
        }
        return result;
    }
});
({
    ///////////////////////////////////////////////////////////////////////////
    // Одна колонка для dataTable, treeTable и т.д.
    // $column
    //  column           - одна колонка
    //  prop             - свойсва класса
    ///////////////////////////////////////////////////////////////////////////
    $column: function (p) {
        let result = null;
        let prop = p.prop;
        let column = p.column;
        //m.sys.breakpoint();
        if ((!prop || prop._visible !== false) && (!column || column._visible !== false)) {
            result = {};
            result.id = m.sys.webixName(p.id);
            if (prop) {
                result.header = prop._header;
                result.editor = 'text';
                result.adjust = true;
                if (prop._datatype == 'bool') {
                    result.editor = 'checkbox'
                }
                else if (prop._datatype == 'date') {
                    result.editor = 'datepicker'
                }
                for (let k in prop) {
                    if (k.substring(0, 1) != '_' && k != 'column') {
                        if (k == 'label') result.header = prop[k];
                        else result[k] = prop[k];
                    }
                }
                if (prop.column) {
                    for (let k in prop.column) {
                        result[k] = prop.column[k];
                    }
                }
            }
            if (column) {
                for (let k in column) {
                    result[k] = column[k];
                }
            }
        }
        return result;
    }
});
