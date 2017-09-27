if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Параметры
///////////////////////////////////////////////////////////////////////////
m.params = {
    set: function (obj, params) {
        if (typeof (obj) == 'string') obj = $$(obj);
        obj._params = params;
    },
    get: function (obj) {
        if (typeof (obj) == 'string') obj = $$(obj);
        while (obj) {
            if (obj._params) return obj._params;
            obj = obj.getParentView();
        }
        return {};
    }
};