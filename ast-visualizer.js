window.renderAst = function (jsonStr) {
    const ast = JSON.parse(jsonStr);
    const container = document.getElementById("astContainer");
    container.innerHTML = "";

    function createNode(node) {
        const el = document.createElement("div");
        el.className = "ast-node";
        el.innerText = node.name;

        if (node.children && node.children.length > 0) {
            const childrenContainer = document.createElement("div");
            childrenContainer.className = "ast-children";

            node.children.forEach(child => {
                childrenContainer.appendChild(createNode(child));
            });

            el.appendChild(childrenContainer);
        }

        return el;
    }

    container.appendChild(createNode(ast));
};
