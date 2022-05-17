using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    //damit der StatsManager das unterscheiden kann
    public string diagramName;

    public UIGridRenderer gridRenderer;
    public DiagramManager diagramManager;
    public Vector2Int gridSize;
    public List<Vector2> points;
    public int scale = 1;

    [SerializeField] float thickness = 10f;

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    protected override void Start()
    {
        gridRenderer = transform.GetComponentInParent<UIGridRenderer>();
        diagramManager = GameObject.FindGameObjectWithTag("DiagramManager").GetComponent<DiagramManager>();
    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float)gridSize.x;
        unitHeight = height / (float)gridSize.y;

        if(points.Count < 2)
        {
            return;
        }

        float angle = 0;

        for(int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];
            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i + 1]) + 45f;
            }

            DrawVerticesForPoint(point, vh, angle);
        }

        for(int i = 0; i<points.Count-1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }
    private void DrawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        /*
        if (point.x > gridRenderer.gridSize.x)
        {
            gridRenderer.gridSize.x *= 2;
        }
        */
        if (point.y > gridRenderer.gridSize.y)
        {
            gridRenderer.gridSize.y *= 2;
            adjustPointScale();
        }
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0,0,angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x , unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);
    }

    private float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    private void Update()
    {
        if(gridRenderer != null)
        {
            if(gridSize != gridRenderer.gridSize)
            {
                gridSize = gridRenderer.gridSize;
                SetVerticesDirty();
            }
        }
    }

    private void adjustPointScale()
    {
        for(int i = 0; i < points.Count; i++)
        {
            points[i] = new Vector2(points[i].x, points[i].y / 2);
        }
        scale *= 2;

        //change scale of text axes
    }
} 