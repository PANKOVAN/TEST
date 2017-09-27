({
    // Upload
    $sysUpload: function (p) {
        let r = {
            commands: {
                cancel: {
                    can: true,
                    exec: function (p) {
                        let uploader = $$('_sysupload_');
                        var id = uploader.files.getFirstId();
                        while (id) {
                            uploader.stopUpload(id);
                            id = uploader.files.getNextId(id);
                        }
                    }
                },
                close: {
                    can: true,
                    exec: function (p) {
                        m.commands.execCommand('cancel', p);
                        m.commands.close(p);
                    }
                },
                addFile: {
                    can: true,
                    exec: function (p) {
                        $$('_sysupload_').fileDialog();
                    }
                },
                upload: {
                    can: true,
                    exec: function (p) {
                        $$('_sysupload_').send();
                    }
                }
            },

            view: 'window',
            modal: true,
            move: true,
            resize: true,
            autoheight: true,
            position: function (state) {
                state.left = state.maxWidth - state.maxWidth / 3 - 100;
                state.top = 100;
                state.width = state.maxWidth / 3;
                state.height = 300;
            },
            head: {
                $pageBar: { name: 'addImage' },
            },
            body: {
                rows: [
                    {
                        view: 'list',
                        id: '_sysuploadlist_',
                        borderless: true,
                        //select: 'multiselect',
                        onClick: { remove_file: function (e, id) { $$('_sysupload_').files.remove(id); } },
                        type: {
                            height: "auto",
                            width: "auto"
                        },
                        template: function (f, type) {
                            //m.sys.bp();
                            let messages = {
                                server: "Done",
                                error: "Error",
                                client: "Ready",
                                transfer: f.percent + "%"
                            };
                            var html = '<table style="width:100%"><tr>'
                                + '<td style="width:60%;">' + f.name + '</td>'
                                + '<td style="width:15%;white-space:nowrap:">' + f.sizetext + '</td>'
                                + '<td style="width:25%">'
                                + '<div style="position:relative">'
                                + '<div style="width:' + (f.status == 'transfer' || f.status == 'server' ? f.percent + '%' : '0px') + ';  background-color:#b8e6ff;">'
                                + '&nbsp;</div>'
                                + '<div style="position:absolute; left:0px; top:0px; width:100%; text-align:center; ">'
                                + {
                                    server: "Загружено",
                                    error: "Ошибка",
                                    client: "",
                                    transfer: f.percent + "%"
                                }[f.status]
                                + '</div>'
                                + '</div>'
                                + '</td>'
                                + '<td>'
                                + '<div class="remove_file"><span class="cancel_icon"></span></div>'
                                + '</td>'
                                + '</tr></table>';
                            return html;

                        }
                    },

                    {
                        view: "uploader",
                        id: "_sysupload_",
                        link: '_sysuploadlist_',
                        height: 1,
                        apiOnly: true,
                        multiple: p.multiple,
                        accept: p.accept,
                        autosend: false,
                        upload: p.url || ((p.commandData && p.commandData.config) ? p.commandData.config.url : ''),
                        urlData: { upload: 1 },
                        on: {
                            onBeforeFileAdd: function (item) {
                                let ud = $$("_sysupload_").data.urlData;
                                ud.id = p.id || ((p.commandData && p.commandData.getSelectedId) ? p.commandData.getSelectedId() : '');
                                ud.name = item.name;
                                ud.type = item.type;
                            },
                            onFileUpload: function (item) {
                                m.commands.refresh(p);
                            },
                            onFileUploadError: function (item) {
                                m.main.error(item._error);
                            }
                        }
                    },

                    { $bottomBar: { commands: ['$addFile', '$upload', '$cancel'] } }
                ]
            }
        };
        return r;
    }
});
