globalThis.mapHelpers = {
    addScaleControl() {
        function attach() {
            const mapInst = globalThis.azureMapsControl?.Core?.getMap() ?? null;
            if (!mapInst) { setTimeout(attach, 300); return; }
            const scale = new atlas.control.ScaleControl({ unit: 'metric', maxWidth: 150 });
            mapInst.controls.add(scale, { position: 'bottom-left' });
        }
        attach();
    }
};
