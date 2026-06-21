globalThis.mapHelpers = {
    _popup: null,
    _map: null,
    _mapClickListenerAttached: false,

    _getMap() {
        if (this._map) return this._map;
        this._map = globalThis.azureMapsControl?.Core?.getMap() ?? null;
        return this._map;
    },

    addScaleControl() {
        function attach() {
            const mapInst = globalThis.azureMapsControl?.Core?.getMap() ?? null;
            if (!mapInst) { setTimeout(attach, 300); return; }
            const scale = new atlas.control.ScaleControl({ unit: 'metric', maxWidth: 150 });
            mapInst.controls.add(scale, { position: 'bottom-left' });
        }
        attach();
    },

    openStorePopup(lng, lat, name, address, distance) {
        const map = this._getMap();
        if (!map) return;

        if (!this._mapClickListenerAttached) {
            map.events.add('click', (e) => {
                if (!this._popup) return;
                const root = document.getElementById('nm-popup-root');
                if (!root) { this.closeStorePopup(); return; }
                const target = e.originalEvent?.target;
                if (target && root.contains(target)) return;
                this.closeStorePopup();
            });
            this._mapClickListenerAttached = true;
        }

        if (this._popup) {
            this._popup.close();
            this._popup = null;
        }

        const content = `
            <div id="nm-popup-root" style="
                position: relative;
                background: var(--surface-elevated, #ffffff);
                border: 1px solid var(--border, #CBD5E1);
                border-radius: 10px;
                box-shadow: 0 4px 20px rgba(0,0,0,0.10), 0 1px 4px rgba(0,0,0,0.06);
                padding: 14px 36px 12px 14px;
                width: 240px;
                font-family: var(--font-body, 'Inter', system-ui, sans-serif);
                animation: nmPopupIn 0.15s ease-out;
            ">
                <button onclick="mapHelpers.closeStorePopup()" aria-label="Close" type="button" style="
                    position: absolute;
                    top: 8px;
                    right: 8px;
                    width: 22px;
                    height: 22px;
                    border: none;
                    background: transparent;
                    border-radius: 50%;
                    cursor: pointer;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    color: var(--text-secondary, #94A3B8);
                    padding: 0;
                    transition: color 0.15s, background 0.15s;
                " onmouseover="this.style.color='var(--text-primary, #475569)';this.style.background='var(--surface, #F1F5F9)'" onmouseout="this.style.color='var(--text-secondary, #94A3B8)';this.style.background='transparent'">
                    <svg width="10" height="10" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                        <line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/>
                    </svg>
                </button>

                <div style="display:flex;align-items:flex-start;gap:7px;margin-bottom:5px;">
                    <svg width="13" height="13" viewBox="0 0 24 24" fill="var(--pin-store, #EF4444)" stroke="none" style="flex-shrink:0;margin-top:2px;">
                        <path d="M12 2C8.13 2 5 5.13 5 9c0 5.25 7 13 7 13s7-7.75 7-13c0-3.87-3.13-7-7-7zm0 9.5a2.5 2.5 0 0 1 0-5 2.5 2.5 0 0 1 0 5z"/>
                    </svg>
                    <span style="
                        font-weight: 600;
                        font-size: 13px;
                        color: var(--text-primary, #1E293B);
                        line-height: 1.3;
                    " title="${name}">${name}</span>
                </div>

                <p style="
                    margin: 0 0 10px 0;
                    font-size: 11.5px;
                    color: var(--text-secondary, #64748B);
                    line-height: 1.5;
                    word-break: break-word;
                ">${address}</p>

                <span style="
                    font-size: 11px;
                    font-family: var(--font-mono, 'JetBrains Mono', monospace);
                    font-weight: 500;
                    color: var(--text-secondary, #94A3B8);
                    letter-spacing: 0.01em;
                ">${distance}</span>

                <svg style="
                    position: absolute;
                    bottom: -9px;
                    left: 50%;
                    transform: translateX(-50%);
                    overflow: visible;
                " width="20" height="10" viewBox="0 0 20 10" xmlns="http://www.w3.org/2000/svg">
                    <path d="M0 0 Q10 10 20 0" fill="var(--border, #CBD5E1)"/>
                    <path d="M1.2 0 Q10 8.5 18.8 0" fill="var(--surface-elevated, #ffffff)"/>
                </svg>
            </div>`;

        this._popup = new atlas.Popup({
            content,
            position: [lng, lat],
            pixelOffset: [0, -42],
            closeButton: false,
            showPointer: false,
            fillColor: 'transparent'
        });

        this._popup.open(map);
    },

    closeStorePopup() {
        if (this._popup) {
            this._popup.close();
            this._popup = null;
        }
    }
};