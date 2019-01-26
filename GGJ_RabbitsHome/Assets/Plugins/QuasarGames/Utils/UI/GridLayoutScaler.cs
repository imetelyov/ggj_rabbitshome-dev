using UnityEngine;
using UnityEngine.UI;   
using System.Collections;

namespace QuasarGames.UI
{
    /**
     * Scale a GridLayoutGroup according to resolution, etc.
     * This is using width-constrained layout
     */
    public class GridLayoutScaler : MonoBehaviour
    {

        private GridLayoutGroup grid;
        private RectOffset gridPadding;
        private RectTransform parent;

        public int cols = 7;
        public float spacing = 10;
        public bool useRelativeSpacing = false;
        [Range(0f, 0.5f)]
        public float spacingRelative = 0.05f;

        private float realSpacing = 10f;

        Vector2 lastSize;

        void Start()
        {
            grid = GetComponent<GridLayoutGroup>();
            parent = GetComponent<RectTransform>();

            realSpacing = spacing;
            if (useRelativeSpacing)
            {
                realSpacing = parent.rect.width * spacingRelative;
            }
            grid.spacing = new Vector2(realSpacing, realSpacing);
            gridPadding = grid.padding;

            lastSize = Vector2.zero;

            UpdateValues();
        }

        // Update is called once per frame
        void Update()
        {
            if (lastSize == parent.rect.size)
            {
                return;
            }

            UpdateValues();
        }

        // Update is called once per frame
        void UpdateValues()
        {
            var paddingX = gridPadding.left + gridPadding.right;

            realSpacing = spacing;
            if (useRelativeSpacing)
            {
                realSpacing = parent.rect.width * spacingRelative;
            }


            var cellSize = Mathf.Round((parent.rect.width - paddingX - (cols - 1) * realSpacing) / cols);
            grid.spacing = new Vector2(realSpacing, realSpacing);
            grid.cellSize = new Vector2(cellSize, cellSize);
        }
    }
}