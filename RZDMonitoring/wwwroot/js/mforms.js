if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Поддержка форм
///////////////////////////////////////////////////////////////////////////
m.forms = {
    setDataObjFromForm: function (form, dataObj) {
        if (form) {
            if (typeof (form) == "string") form = $$(form);
            if (typeof (dataObj) == "string") dataObj = $$(dataObj);
            if (dataObj && dataObj.getSelectedItem) {
                let item = dataObj.getSelectedItem();
                if (item) {
                    let values = form.getValues();
                    for (let index in values) {
                        if (index[0] != '$' && index[0] != '_' && index != 'id') {
                            let value = values[index];
                            item[index] = value;
                        }
                    }
                    dataObj.refresh();
                    let dp = webix.dp(dataObj);
                    if (dp) dp.setItemState(item.id, "update");
                    //dataObj.updateItem(item.id, item);
                    //let dp = webix.dp(dataObj);
                    //if (dp) dp.save(item.id, "update");
                    //let item1 = item;
                }
            }

        }
    },
    addDataObjFromForm: function (form, dataObj, addChild) {
        if (form) {
            if (typeof (form) == "string") form = $$(form);
            if (typeof (dataObj) == "string") dataObj = $$(dataObj);
            if (dataObj && dataObj.getSelectedItem) {
                let curPos = null;
                let curObj = dataObj.getSelectedItem();
                let curId = dataObj.getSelectedId();
                if (curId) curPos = dataObj.getIndexById(curId);
                if (curPos) curPos++;
                let id = null;
                if (addChild) {
                    dataObj.open(curId);
                    id = dataObj.add(form.getValues(), 0, curId);
                }
                else if (curObj && curObj.$parent) {
                    id = dataObj.add(form.getValues(), curPos, curObj.$parent);
                }
                else {
                    id = dataObj.add(form.getValues(), curPos);
                }
                dataObj.select(id);
                dataObj.showItem(id);
            }
        }
    },
    setFormFromDataObj: function (form, dataObj) {
        if (form) {
            if (typeof (form) == "string") form = $$(form);
            if (typeof (dataObj) == "string") dataObj = $$(dataObj);

            if (dataObj && dataObj.getSelectedItem) {
                let item = dataObj.getSelectedItem();
                if (item) {
                    form.setValues(item);
                }
            }

        }
    }
};