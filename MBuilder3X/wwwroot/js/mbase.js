if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// BASE
///////////////////////////////////////////////////////////////////////////
m.base = {
    MEDMIdObj: {
        bestImg: function () {
            for (let id in this.arts) {
                let art = this.arts[id];
                if (art.artTypeId == 'IMG') {
                    return '/mstore/' + art.artPath;
                }
            }
            return '';
        },
        prjImg: function () {
            for (let id in this.arts) {
                let art = this.arts[id];
                if (art.artTypeId == 'IMG') {
                    return '/mstore/' + art.artPath;
                }
            }
            return '/images/projects/noname.png';
        }
    },
    MEDMNameObj: {},
    MEDMGuidObj: {},
    MEDMCfgObj: {
        _isCfg: true,
        getIcon: function () {
            if (this.icon) return 'images/types/icons8-' + this.icon + '.png';
            m.main.error('Для ['+this.id+'] '+this.header+' ('+this._type+') аттрибут icon не задан');
            return '';
        },

    }
};