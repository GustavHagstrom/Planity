window.ganttResizer = {
    init: function (dotNetRef, mouseEvent) {
        let startX = mouseEvent.clientX;
        let leftElem = document.querySelector('.gantt-left');
        let startWidth = leftElem.offsetWidth;

        function onMouseMove(e) {
            let newWidth = startWidth + (e.clientX - startX);
            dotNetRef.invokeMethodAsync('SetLeftWidth', newWidth);
        }

        function onMouseUp() {
            document.removeEventListener('mousemove', onMouseMove);
            document.removeEventListener('mouseup', onMouseUp);
        }

        document.addEventListener('mousemove', onMouseMove);
        document.addEventListener('mouseup', onMouseUp);
    }
};
