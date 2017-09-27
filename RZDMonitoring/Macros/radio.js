({
    ///////////////////////////////////////////////////////////////////////////
    // button
    // $button
    //  id              - id
    //  options         - options для radio
    ///////////////////////////////////////////////////////////////////////////
    $radio: function (p) {
        return {
            id: p.id,
            view: "radio",
            value: p.options[0].id,
            options: p.options,
            visible: (p.options && p.options.lengtj > 1)
        }
    }
});
