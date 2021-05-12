using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class MySceneGraph : SceneGraph<EventGraph> {
  private void Awake() {
    graph = graph.Copy() as EventGraph;
    // graph.transformTarget = this;
  }
}
