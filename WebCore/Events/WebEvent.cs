namespace WebCore.Events;

public enum WebEvent
{
    // Mouse Events
    OnClick,
    OnDblClick,
    OnMouseDown,
    OnMouseUp,
    OnMouseMove,
    OnMouseOver,
    OnMouseOut,
    OnContextMenu,

    // Keyboard Events
    OnKeyDown,
    OnKeyUp,
    OnKeyPress,

    // Form / Input Events
    OnChange,
    OnInput,
    OnFocus,
    OnBlur,
    OnSubmit,
    OnReset,
    OnSelect,

    // Clipboard Events
    OnCopy,
    OnCut,
    OnPaste,

    // Drag & Drop Events
    OnDragStart,
    OnDragOver,
    OnDragEnter,
    OnDragLeave,
    OnDrop,
    OnDragEnd,

    // Window / Document Events
    OnLoad,
    OnUnload,
    OnResize,
    OnScroll,

    // Touch / Pointer Events
    OnTouchStart,
    OnTouchMove,
    OnTouchEnd,
    OnPointerDown,
    OnPointerUp,
    OnPointerMove,
    OnPointerEnter,
    OnPointerLeave,
    
    // Custom
    Change,
    Click,
    Close,
    Maximize,
    Minimize,
    Restore,
}